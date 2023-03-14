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
using webapi.Models.DTO.TrainingprogramDTO;
using webapi.Services.TrainingprogramServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TrainingprogramsController : ControllerBase
    {
        private readonly ITrainingprogramService _service;
        private readonly IMapper _mapper;

        public TrainingprogramsController(ITrainingprogramService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Trainingprograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainingprogram>>> GetTrainingprograms()
        {
            return Ok(_mapper.Map<ICollection<TrainingprogramReadDto>>(await _service.GetAll()));
        }

        // GET: api/Trainingprograms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trainingprogram>> GetTrainingprogram(int id)
        {
            try
            {
                return Ok(_mapper.Map<TrainingprogramReadDto>(await _service.GetById(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        // PUT: api/Trainingprograms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutTrainingprogram(int id, TrainingprogramUpdateDto trainingprogramUpdateDto)
        {
            if (id != trainingprogramUpdateDto.Id)
            {
                return BadRequest();
            }


            try
            {
                var trainingprogram = _mapper.Map<Trainingprogram>(trainingprogramUpdateDto);
                await _service.Update(trainingprogram);
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

        // POST: api/Trainingprograms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trainingprogram>> PostTrainingprogram(TrainingprogramCreateDto trainingprogramCreateDto)
        {
            var trainingprogram = _mapper.Map<Trainingprogram>(trainingprogramCreateDto);
            await _service.Create(trainingprogram);
            var trainingprogramUpdateWorkoutsDto = new TrainingprogramUpdateWorkoutsDto { WorkoutIds = trainingprogramCreateDto.WorkoutIds };
            var trainingprogramUpdateCategoriesDto = new TrainingprogramUpdateCategoriesDto { CategoryIds = trainingprogramCreateDto.CategoryIds };
            await _service.UpdateTrainingprogramWorkouts(trainingprogram.Id, trainingprogramUpdateWorkoutsDto.WorkoutIds);
            await _service.UpdateTrainingprogramCategories(trainingprogram.Id, trainingprogramUpdateCategoriesDto.CategoryIds);

            return CreatedAtAction(nameof(GetTrainingprogram), new { id = trainingprogram.Id }, trainingprogram);
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
