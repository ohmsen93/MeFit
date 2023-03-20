using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;
using webapi.Models.DTO.ExerciseDTO;
using webapi.Services.ExerciseServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _service;
        private readonly IMapper _mapper;

        public ExercisesController(IExerciseService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercises()
        {
            return Ok(_mapper.Map<ICollection<ExerciseReadDto>>(await _service.GetAll()));
        }

        // GET: api/Exercises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> GetExercise(int id)
        {
            try
            {
                return Ok(_mapper.Map<ExerciseReadDto>(await _service.GetById(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        // PUT: api/Exercises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutExercise(int id, ExerciseUpdateDto exerciseUpdateDto)
        {
            if (id != exerciseUpdateDto.Id)
            {
                return BadRequest();
            }


            try
            {
                var exercise = _mapper.Map<Exercise>(exerciseUpdateDto);
                await _service.Update(exercise);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

            return NoContent();
        }

        // POST: api/Exercises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(ExerciseCreateDto exerciseCreateDto)
        {
            var exercise = _mapper.Map<Exercise>(exerciseCreateDto);
            await _service.Create(exercise);
            var exerciseUpdateSetsDto = new ExerciseUpdateSetsDto { SetIds = exerciseCreateDto.SetIds };
            var exerciseUpdateMusclegroupsDto = new ExerciseUpdateMusclegroupsDto { MusclegroupIds = exerciseCreateDto.MusclegroupIds };
            await _service.UpdateExerciseSets(exercise.Id, exerciseUpdateSetsDto.SetIds);
            await _service.UpdateExerciseMusclegroups(exercise.Id, exerciseUpdateMusclegroupsDto.MusclegroupIds);

            return CreatedAtAction(nameof(GetExercise), new { id = exercise.Id }, exercise);
        }


        //// DELETE: api/Exercises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            try
            {
                await _service.DeleteById(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

            return NoContent();
        }
    }
}
