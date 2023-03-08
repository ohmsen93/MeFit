//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mime;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using webapi.DatabaseContext;
//using webapi.Models;
//using webapi.Services.SetService;

//namespace webapi.Controllers
//{
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    [Produces(MediaTypeNames.Application.Json)]
//    [Consumes(MediaTypeNames.Application.Json)]
//    [ApiConventionType(typeof(DefaultApiConventions))]
//    public class SetsController : ControllerBase
//    {
//        private readonly ISetService _service;

//        public SetsController(ISetService service)
//        {
//            _service = service;
//        }

//        // GET: api/Sets
//        //[HttpGet]
//        //public async Task<ActionResult<IEnumerable<Set>>> GetSets()
//        //{
//        //  //if (_service.Sets == null)
//        //  //{
//        //  //    return NotFound();
//        //  //}
//        //  //  return await _service.Sets.ToListAsync();
//        //}

//        // GET: api/Sets/5
//        //[HttpGet("{id}")]
//        //public async Task<ActionResult<Set>> GetSet(int id)
//        //{
//          //if (_service.Sets == null)
//          //{
//          //    return NotFound();
//          //}
//          //  var @set = await _service.Sets.FindAsync(id);

//          //  if (@set == null)
//          //  {
//          //      return NotFound();
//          //  }

//          //  return @set;
//        //}

//        // PUT: api/Sets/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        //[HttpPut("{id}")]
//        //public async Task<IActionResult> PutSet(int id, Set @set)
//        //{
//        //    if (id != @set.Id)
//        //    {
//        //        return BadRequest();
//        //    }

//        //    _service.Entry(@set).State = EntityState.Modified;

//        //    try
//        //    {
//        //        await _service.SaveChangesAsync();
//        //    }
//        //    catch (DbUpdateConcurrencyException)
//        //    {
//        //        if (!SetExists(id))
//        //        {
//        //            return NotFound();
//        //        }
//        //        else
//        //        {
//        //            throw;
//        //        }
//        //    }

//        //    return NoContent();
//        //}

//        // POST: api/Sets
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        //[HttpPost]
//        //public async Task<ActionResult<Set>> PostSet(Set @set)
//        //{
//        //  if (_service.Sets == null)
//        //  {
//        //      return Problem("Entity set 'MeFitContext.Sets'  is null.");
//        //  }
//        //    _service.Sets.Add(@set);
//        //    await _service.SaveChangesAsync();

//        //    return CreatedAtAction("GetSet", new { id = @set.Id }, @set);
//        //}

//        //// DELETE: api/Sets/5
//        //[HttpDelete("{id}")]
//        //public async Task<IActionResult> DeleteSet(int id)
//        //{
//        //    if (_service.Sets == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    var @set = await _service.Sets.FindAsync(id);
//        //    if (@set == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    _service.Sets.Remove(@set);
//        //    await _service.SaveChangesAsync();

//        //    return NoContent();
//        //}

//        //private bool SetExists(int id)
//        //{
//        //    return (_service.Sets?.Any(e => e.Id == id)).GetValueOrDefault();
//        //}
//    }
//}
