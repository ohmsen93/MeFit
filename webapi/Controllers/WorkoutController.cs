using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;
using webapi.Models.DTO.GoalDTO;
using webapi.Models.DTO.WorkoutDTO;
using webapi.Services.WorkoutServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _service;
        private readonly IMapper _mapper;

        public WorkoutsController(IWorkoutService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all workouts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles ="Regular")]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkouts()
        {
            return Ok(_mapper.Map<ICollection<WorkoutReadDto>>(await _service.GetAll(User.FindFirstValue(ClaimTypes.NameIdentifier))));
        }
        /// <summary>
        /// Gets a workout by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetWorkout(int id)
        {
            try
            {
                return Ok(_mapper.Map<WorkoutReadDto>(await _service.GetById(id)));
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
        /// Gets workouts by trainingprogram id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Workouts/ByTrainingProgramId/5
        [HttpGet("trainingprograms/{id}")]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkoutsByTrainingprogramId(int id)
        {
            try
            {
                var workouts = await _service.GetWorkoutsByTrainingprogramId(id);
                return Ok(_mapper.Map<IEnumerable<WorkoutReadDto>>(workouts));
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
        /// Updates a workout by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workoutUpdateDto"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchWorkout(int id, WorkoutUpdateDto workoutUpdateDto)
        {

            if (id != workoutUpdateDto.Id)
            {
                return BadRequest();
            }     

            try
            {
                var workout = _mapper.Map<Workout>(workoutUpdateDto);
                await _service.Update(workout);
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
        /// Creates a new workout 
        /// </summary>
        /// <param name="workoutCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Workout>> PostWorkout(WorkoutCreateDto workoutCreateDto)
        {
            var workout = _mapper.Map<Workout>(workoutCreateDto);
            await _service.Create(workout,workoutCreateDto.ExerciseIds);

            //var workoutUpdateExercisesDto = new WorkoutUpdateExercisesDto { ExerciseIds = workoutCreateDto.ExerciseIds };
            //await _service.UpdateWorkoutExercises(workout.Id, workoutUpdateExercisesDto.ExerciseIds);
            
            var WorkoutReadDto = _mapper.Map<WorkoutReadDto>(workout);
            return CreatedAtAction(nameof(GetWorkout), new { id = workout.Id }, WorkoutReadDto);
        }


        /// <summary>
        /// Delets a workout by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
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
        /// Updates workouts exercises by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workoutUpdateExercisesDto"></param>
        /// <returns></returns>
        [HttpPatch("{id}/exercises")]
        public async Task<IActionResult> PatchWorkoutExercises(int id, WorkoutUpdateExercisesDto workoutUpdateExercisesDto)
        {           
            try
            {                
                await _service.UpdateWorkoutExercises(id, workoutUpdateExercisesDto.Exercises);
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
