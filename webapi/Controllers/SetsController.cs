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
using webapi.Models.DTO.SetDTO;
using webapi.Services.SetServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SetsController : ControllerBase
    {
        private readonly ISetService _service;
        private readonly IMapper _mapper;

        public SetsController(ISetService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Sets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Set>>> GetSets()
        {
            return Ok(_mapper.Map<ICollection<SetReadDto>>(await _service.GetAll()));
        }

        // GET: api/Sets/5
        [HttpGet("{id}")]
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

        // PATCH: api/Sets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
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

        // POST: api/Sets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Set>> PostSet(SetCreateDto setCreateDto)
        {
            var set = _mapper.Map<Set>(setCreateDto);
            await _service.Create(set);
            return CreatedAtAction(nameof(GetSet), new { id = set.Id }, set);
        }

        //// DELETE: api/Sets/5
        [HttpDelete("{id}")]
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
    }
}
