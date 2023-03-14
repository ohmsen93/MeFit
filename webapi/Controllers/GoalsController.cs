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
using webapi.Models.DTO.WorkoutDTO;
using webapi.Services.GoalServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalService _service;
        private readonly IMapper _mapper;

        public GoalsController(IGoalService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #region basic CRUD
        // GET: api/Goals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goal>>> GetGoals()
        {
            return Ok(_mapper.Map<ICollection<GoalReadDto>>(await _service.GetAll()));
        }

        // GET: api/Goals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Goal>> GetGoal(int id)
        {
            try
            {
                return Ok(_mapper.Map<GoalReadDto>(await _service.GetById(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        // PUT: api/Goals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutGoal(int id, GoalUpdateDto goalUpdateDto)
        {
            if (id != goalUpdateDto.Id)
            {
                return BadRequest();
            }


            try
            {
                var goal = _mapper.Map<Goal>(goalUpdateDto);
                await _service.Update(goal);
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

        // POST: api/Goals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Goal>> PostGoal(GoalCreateDto goalCreateDto)
        {
            var goal = _mapper.Map<Goal>(goalCreateDto);
            await _service.Create(goal);
            return CreatedAtAction(nameof(GetGoal), new { id = goal.Id }, goal);
        }

        //// DELETE: api/Goals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal(int id)
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
        #endregion

        /// <summary>
        /// Gets completed goals of a specific user by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("completed/user/{id}")]
        public async Task<ActionResult<IEnumerable<Goal>>> GetCompletedGoals(int id)
        {
            return Ok(_mapper.Map<ICollection<GoalReadDto>>(await _service.GetCompletedGoals(id)));
        }

        /// <summary>
        /// Gets workouts of a specific goal by goal id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/workouts")]
        public async Task<ActionResult<IEnumerable<Workout>>> GetGoalWorkouts(int id)
        {
            return Ok(_mapper.Map<ICollection<WorkoutReadDto>>(await _service.GetGoalWorkouts(id)));
        }

        /// <summary>
        /// Gets completed workouts of a goal by goal id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/completedworkouts")]
        public async Task<ActionResult<IEnumerable<Workout>>> GetGoalCompletedWorkouts(int id)
        {
            return Ok(_mapper.Map<ICollection<WorkoutReadDto>>(await _service.GetGoalCompletedWorkouts(id)));
        }

    }
}
