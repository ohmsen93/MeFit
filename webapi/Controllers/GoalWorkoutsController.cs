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
using webapi.Models.DTO.GoalWorkoutDTO;
using webapi.Services;
using webapi.Services.GaolWrokutServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GoalWorkoutsController : ControllerBase
    {
        private readonly IGoalWorkoutService _service;
        private readonly IMapper _mapper;

        public GoalWorkoutsController(IGoalWorkoutService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/GoalWorkouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoalWorkouts>>> GetGoalWorkouts()
        {
            return Ok(_mapper.Map<ICollection<GoalWorkoutReadDto>>(await _service.GetAll()));
        }

        // GET: api/GoalWorkouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GoalWorkouts>> GetGoalWorkout(int id)
        {
            return Ok(_mapper.Map<GoalWorkoutReadDto>(await _service.GetById(id)));
        }

        // PUT: api/GoalWorkouts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchGoalWorkout(int id, GoalWorkoutUpdateDto goalWorkoutUpdateDto)
        {
            if (id != goalWorkoutUpdateDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var goalWorkout = _mapper.Map<GoalWorkouts>(goalWorkoutUpdateDto);
                await _service.Update(goalWorkout);
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

        // POST: api/GoalWorkouts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GoalWorkouts>> PostGoalWorkout(GoalWorkoutCreateDto goalWorkoutCreateDto)
        {
            var goalWorkout = _mapper.Map<GoalWorkouts>(goalWorkoutCreateDto);
            await _service.Create(goalWorkout);
            return CreatedAtAction(nameof(GetGoalWorkout), new { id = goalWorkout.Id }, goalWorkout);
        }

        // DELETE: api/GoalWorkouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoalWorkout(int id)
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
