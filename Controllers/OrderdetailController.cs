
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Northwind.MySQL.Controllers;



[ApiController]
[Route("api/[controller]")]
public class OrderdetailController : ControllerBase
{
    private readonly NorthwindContext _context;

    public OrderdetailController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Orderdetail>>> GetOrderdetails()
    {
        var Orderdetails = await _context.Orderdetails
            .ToListAsync();

        return Orderdetails;
    }

    [HttpGet("id")]
    public async Task<ActionResult<Orderdetail>> GetOrderdetail(int id)
    {
        var Orderdetail = await _context.Orderdetails.FindAsync(id);
        if (Orderdetail == null)
        {
            return NotFound();
        }
        return Orderdetail;
    }

      [HttpPost]
        public async Task<ActionResult<Orderdetail>> PostOrderdetail(Orderdetail Orderdetail)
        {
            _context.Orderdetails.Add(Orderdetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderdetail), new {id = Orderdetail}, Orderdetail);
        }
   [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderdetail(int id,Orderdetail Orderdetail)
        {
            if (id != Orderdetail.OrderDetailsId)
            {
                return BadRequest();
            }

            _context.Entry(Orderdetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderdetailExists(id))
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
        public async Task<ActionResult> DeleteOrderdetail(int id)
{
    var Orderdetail = await _context.Orderdetails.FindAsync(id);
    if (Orderdetail == null)
    {
        return NotFound();
    }

    _context.Orderdetails.Remove(Orderdetail);
    await _context.SaveChangesAsync();

    return NoContent();
}

    private bool OrderdetailExists(int id)
    {
        return _context.Orderdetails.Any(e => e.OrderDetailsId == id);
    }

}
