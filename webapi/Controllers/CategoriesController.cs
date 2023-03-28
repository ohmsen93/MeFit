using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using webapi.Exceptions;
using webapi.Models;
using webapi.Services.CategoryServices;
using webapi.Models.DTO.CategoryDTO;
using Microsoft.AspNetCore.Authorization;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #region basic CRUD
        /// <summary>
        /// Gets all categoriess
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoríes()
        {
            return Ok(_mapper.Map<ICollection<CategoryReadDto>>(await _service.GetAll()));
        }

        /// <summary>
        /// Gets a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                return Ok(_mapper.Map<CategoryReadDto>(await _service.GetById(id)));
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
        /// Updates a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryUpdateDto"></param>
        /// <returns></returns>                
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            if (id != categoryUpdateDto.Id)
            {
                return BadRequest();
            }


            try
            {
                var category = _mapper.Map<Category>(categoryUpdateDto);
                await _service.Update(category);
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
        /// Creates a new category
        /// </summary>
        /// <param name="categoryCreateDto"></param>
        /// <returns></returns>        
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _service.Create(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        /// <summary>
        /// Delets a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
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
