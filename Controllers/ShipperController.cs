
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Northwind.MySQL.Controllers;



[ApiController]
[Route("api/[controller]")]
public class ShipperController : ControllerBase
{
    private readonly NorthwindContext _context;

    public ShipperController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shipper>>> GetShippers()
    {
        var Shippers = await _context.Shippers
            .ToListAsync();

        return Shippers;
    }

    [HttpGet("id")]
    public async Task<ActionResult<Shipper>> GetShipper(int id)
    {
        var Shipper = await _context.Shippers.FindAsync(id);
        if (Shipper == null)
        {
            return NotFound();
        }
        return Shipper;
    }

      [HttpPost]
        public async Task<ActionResult<Shipper>> PostShipper(Shipper Shippers)
        {
            _context.Shippers.Add(Shippers);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShipper), new {id = Shippers}, Shippers);
        }
   [HttpPut("{id}")]
        public async Task<IActionResult> PutShipper(int id,Shipper Shipper)
        {
            if (id != Shipper.ShipperId)
            {
                return BadRequest();
            }

            _context.Entry(Shipper).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipperExists(id))
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
public async Task<ActionResult> DeleteShipper(int id)
{
    var Shippers = await _context.Shippers.FindAsync(id);
    if (Shippers == null)
    {
        return NotFound();
    }

    _context.Shippers.Remove(Shippers);
    await _context.SaveChangesAsync();

    return NoContent();
}

    private bool ShipperExists(int id)
    {
        return _context.Shippers.Any(e => e.ShipperId == id);
    }

}
