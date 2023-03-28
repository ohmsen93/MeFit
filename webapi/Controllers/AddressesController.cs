using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
using webapi.Models.DTO.AddressDTO;
using webapi.Services.AddressServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _service;
        private readonly IMapper _mapper;

        public AddressesController(IAddressService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #region basic CRUD
        /// <summary>
        /// Gets all addresses
        /// </summary>
        /// <param name="addressReadDto"></param>
        /// <returns></returns>
        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            //Method 1
            var subjectFoo = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(_mapper.Map<ICollection<AddressReadDto>>(await _service.GetAll()));
        }

        /// <summary>
        /// Gets an address by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressReadDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates an address by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressUpdateDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new address
        /// </summary>
        /// <param name="addressCreateDto"></param>
        /// <returns></returns>
        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(AddressCreateDto addressCreateDto)
        {
            var address = _mapper.Map<Address>(addressCreateDto);
            await _service.Create(address);
            return CreatedAtAction(nameof(GetAddress), new { id = address.Id }, address);
        }

        /// <summary>
        /// Delets a address by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Addresses/5
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
        #endregion
    }
}
