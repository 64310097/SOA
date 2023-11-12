
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Northwind.MySQL.Controllers;



[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly NorthwindContext _context;

    public CategoryController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var Categories = await _context.Categories
            .ToListAsync();

        return Categories;
    }

    [HttpGet("id")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var Category = await _context.Categories.FindAsync(id);
        if (Category == null)
        {
            return NotFound();
        }
        return Category;
    }

      [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category Categories)
        {
            _context.Categories.Add(Categories);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new {id = Categories}, Categories);
        }
   [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id,Category Category)
        {
            if (id != Category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(Category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
public async Task<ActionResult> DeleteCategory(int id)
{
    var Categories = await _context.Categories.FindAsync(id);
    if (Categories == null)
    {
        return NotFound();
    }

    _context.Categories.Remove(Categories);
    await _context.SaveChangesAsync();

    return NoContent();
}

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.CategoryId == id);
    }

}
