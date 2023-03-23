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
using webapi.Models.DTO.GoalDTO;
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

        /// <summary>
        /// Gets all trainingprograms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainingprogram>>> GetTrainingprograms()
        {
            return Ok(_mapper.Map<ICollection<TrainingprogramReadDto>>(await _service.GetAll()));
        }

        /// <summary>
        /// Gets a trainingprogram by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates a trainingprogram by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainingprogramUpdateDto"></param>
        /// <returns></returns>        
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

        /// <summary>
        /// Creates a new trainingprogram
        /// </summary>
        /// <param name="trainingprogramCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Trainingprogram>> PostTrainingprogram(TrainingprogramCreateDto trainingprogramCreateDto)
        {
            var trainingprogram = _mapper.Map<Trainingprogram>(trainingprogramCreateDto);

            await _service.Create(trainingprogram,trainingprogramCreateDto.WorkoutIds,trainingprogramCreateDto.CategoryIds);
            var trainingprogramReadDto = _mapper.Map<TrainingprogramReadDto>(trainingprogram);
            return CreatedAtAction(nameof(GetTrainingprogram), new { id = trainingprogram.Id }, trainingprogramReadDto);
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


        /// <summary>
        /// Updates trainingporgram workouts by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainingprogramUpdateWorkoutsDto"></param>
        /// <returns></returns>
        [HttpPatch("{id}/workouts")]
        public async Task<IActionResult> PatchTrainingprogramWorkouts(int id, TrainingprogramUpdateWorkoutsDto trainingprogramUpdateWorkoutsDto)
        {
            try
            {          
                await _service.UpdateTrainingprogramWorkouts(id, trainingprogramUpdateWorkoutsDto.Workouts);
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

        /// <summary>
        /// Updates trainingporgram categories by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainingprogramUpdateCategoriesDto"></param>
        /// <returns></returns>
        [HttpPatch("{id}/categories")]
        public async Task<IActionResult> PatchTrainingprogramCategories(int id, TrainingprogramUpdateCategoriesDto trainingprogramUpdateCategoriesDto)
        {
            try
            {
                await _service.UpdateTrainingprogramCategories(id, trainingprogramUpdateCategoriesDto.Categories);
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
