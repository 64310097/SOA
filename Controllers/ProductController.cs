using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Northwind.MySQL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly NorthwindContext _context;

    public ProductController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _context.Products
            .ToListAsync();

        return products;
    }

    [HttpGet("id")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

   [HttpPut("{id}")]
        public async Task<IActionResult> Putproduct(int id,Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
public async Task<ActionResult> DeleteProduct(int id)
{
    var products = await _context.Products.FindAsync(id);
    if (products == null)
    {
        return NotFound();
    }

    _context.Products.Remove(products);
    await _context.SaveChangesAsync();

    return NoContent();
}
    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.ProductId == id);
    }

  
}

  