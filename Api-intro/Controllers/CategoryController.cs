using Api_intro.Data;
using Api_intro.DIOs.Category;
using Api_intro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult>  GetAll()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int? id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category category = new()
            {
                Name = request.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            Category category= await _context.Categories.FirstOrDefaultAsync(m=>m.Id== id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Edit(CategoryEditDto request,int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category category= await _context.Categories.FirstOrDefaultAsync(m=>m.Id== id);
            if (category == null) return NotFound();

            category.Name = request.Name;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term is required.");
            }

            List<Category> categories = await _context.Categories
                .Where(c => c.Name.Trim().ToLower().Contains(searchTerm.ToLower().Trim()))
                .ToListAsync();

            if (categories.Count == 0)
            {
                return NotFound("No matching categories found.");
            }

            return Ok(categories);
        }

    }
}
