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
using webapi.Models.DTO.ContributionrequestDTO;
using webapi.Services.ContributionrequestServices;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ContributionrequestsController : ControllerBase
    {
        private readonly IContributionrequestService _service;
        private readonly IMapper _mapper;

        public ContributionrequestsController(IContributionrequestService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Contributionrequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contributionrequest>>> GetContributionrequests()
        {
            return Ok(_mapper.Map<ICollection<ContributionrequestReadDto>>(await _service.GetAll()));
        }

        // GET: api/Contributionrequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contributionrequest>> GetContributionrequest(int id)
        {
            try
            {
                return Ok(_mapper.Map<ContributionrequestReadDto>(await _service.GetById(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        // PATCH: api/Contributionrequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutContributionrequest(int id, ContributionrequestUpdateDto contributionrequestUpdateDto)
        {
            if (id != contributionrequestUpdateDto.Id)
            {
                return BadRequest();
            }


            try
            {
                var contributionrequest = _mapper.Map<Contributionrequest>(contributionrequestUpdateDto);
                await _service.Update(contributionrequest);
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

        // POST: api/Contributionrequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contributionrequest>> PostContributionrequest(ContributionrequestCreateDto contributionrequestCreateDto)
        {
            var contributionrequest = _mapper.Map<Contributionrequest>(contributionrequestCreateDto);
            await _service.Create(contributionrequest);
            return CreatedAtAction(nameof(GetContributionrequest), new { id = contributionrequest.Id }, contributionrequest);
        }

        //// DELETE: api/Contributionrequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContributionrequest(int id)
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
