using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;
using webapi.Models.DTO.SetDTO;
using webapi.Services.SetServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]
    public class SetsController : ControllerBase
    {
        private readonly ISetService _service;
        private readonly IMapper _mapper;

        public SetsController(ISetService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #region basic CRUD
        /// <summary>
        /// Gets all sets
        /// </summary>
        /// <returns></returns>
        // GET: api/Sets
        [HttpGet]
        [Authorize(Roles = "Admin,Contributor")]
        public async Task<ActionResult<IEnumerable<Set>>> GetSets()
        {
            return Ok(_mapper.Map<ICollection<SetReadDto>>(await _service.GetAll()));
        }

        /// <summary>
        /// Gets a set by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Sets/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Contributor")]
        public async Task<ActionResult<Set>> GetSet(int id)
        {
            try
            {
                return Ok(_mapper.Map<SetReadDto>(await _service.GetById(id)));
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
        /// Updates a set by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="setUpdateDto"></param>
        /// <returns></returns>
        // PATCH: api/Sets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin,Contributor")]
        public async Task<IActionResult> PutSet(int id, SetUpdateDto setUpdateDto)
        {
            if (id != setUpdateDto.Id)
            {
                return BadRequest();
            }


            try
            {
                var set = _mapper.Map<Set>(setUpdateDto);
                await _service.Update(set);
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
        /// Creates a new set
        /// </summary>
        /// <param name="setCreateDto"></param>
        /// <returns></returns>
        // POST: api/Sets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin,Contributor")]
        public async Task<ActionResult<Set>> PostSet(SetCreateDto setCreateDto)
        {
            var set = _mapper.Map<Set>(setCreateDto);
            await _service.Create(set);
            return CreatedAtAction(nameof(GetSet), new { id = set.Id }, set);
        }

        /// <summary>
        /// Delets a set by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Sets/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Contributor")]
        public async Task<IActionResult> DeleteSet(int id)
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
    }
}
