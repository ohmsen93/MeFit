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
using webapi.Models.DTO.AddressDTO;
using webapi.Services.AddressServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _service;
        private readonly IMapper _mapper;

        public AddressesController(IAddressService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Addresses
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return Ok(_mapper.Map<ICollection<AddressReadDto>>(await _service.GetAll()));
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            try
            {
                return Ok(_mapper.Map<AddressReadDto>(await _service.GetById(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        // PATCH: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutAddress(int id, AddressUpdateDto addressUpdateDto)
        {
            if (id != addressUpdateDto.Id)
            {
                return BadRequest();
            }


            try
            {
                var address = _mapper.Map<Address>(addressUpdateDto);
                await _service.Update(address);
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

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(AddressCreateDto addressCreateDto)
        {
            var address = _mapper.Map<Address>(addressCreateDto);
            await _service.Create(address);
            return CreatedAtAction(nameof(GetAddress), new { id = address.Id }, address);
        }

        //// DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
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
