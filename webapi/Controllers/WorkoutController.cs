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

        // GET: api/Workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkouts()
        {
            return Ok(_mapper.Map<ICollection<WorkoutReadDto>>(await _service.GetAll()));
        }

        // GET: api/Workouts/5
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

        // PUT: api/Workouts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Workouts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Workout>> PostWorkout(WorkoutCreateDto workoutCreateDto)
        {
            var workout = _mapper.Map<Workout>(workoutCreateDto);
            await _service.Create(workout);
            var workoutUpdateExercisesDto = new WorkoutUpdateExercisesDto { ExerciseIds = workoutCreateDto.ExerciseIds };
            await _service.UpdateWorkoutExercises(workout.Id, workoutUpdateExercisesDto.ExerciseIds);

            return CreatedAtAction(nameof(GetWorkout), new { id = workout.Id }, workout);
        }


        //// DELETE: api/Workouts/5
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
    }
}
