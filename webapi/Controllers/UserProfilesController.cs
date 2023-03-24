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
using webapi.Models.DTO.UserProfileDTO;
using webapi.Services.UserProfileServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserProfilesController : ControllerBase
    {
        private readonly IUserProfileService _service;
        private readonly IMapper _mapper;

        public UserProfilesController(IUserProfileService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #region basic CRUD
        /// <summary>
        /// Gets all userprofiles
        /// </summary>
        /// <returns></returns>
        // GET: api/UserProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles()
        {
            return Ok(_mapper.Map<IEnumerable<UserProfileReadDto>>(await _service.GetAll()));
        }

        /// <summary>
        /// Gets a userprofile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/UserProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int id)
        {
            try
            {
                return Ok(_mapper.Map<UserProfileReadDto>(await _service.GetById(id)));
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
        /// Updates a userprofile by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userProfileUpdateDto"></param>
        /// <returns></returns>
        // Patch: api/UserProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutUserProfile(int id, UserProfileUpdateDto userProfileUpdateDto)
        {
            if (id != userProfileUpdateDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var userProfile = _mapper.Map<UserProfile>(userProfileUpdateDto);
                await _service.Update(userProfile);
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
        /// Creates a new userprofile
        /// </summary>
        /// <param name="userProfileCreateDto"></param>
        /// <returns></returns>
        // POST: api/UserProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProfile>> PostUserProfile(UserProfileCreateDto userProfileCreateDto)
        {
            var userProfile = _mapper.Map<UserProfile>(userProfileCreateDto);
            await _service.Create(userProfile);
            return CreatedAtAction(nameof(GetUserProfile), new { id = userProfile.Id }, userProfile);
        }

        /// <summary>
        /// Delets a userprofile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/UserProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
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
