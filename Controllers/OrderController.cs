
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Northwind.MySQL.Controllers;



[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly NorthwindContext _context;

    public OrderController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var Orders = await _context.Orders
            .ToListAsync();

        return Orders;
    }

    [HttpGet("id")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var Order = await _context.Orders.FindAsync(id);
        if (Order == null)
        {
            return NotFound();
        }
        return Order;
    }

      [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order Orders)
        {
            _context.Orders.Add(Orders);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new {id = Orders}, Orders);
        }
   [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id,Order Order)
        {
            if (id != Order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
public async Task<ActionResult> DeleteOrder(int id)
{
    var Orders = await _context.Orders.FindAsync(id);
    if (Orders == null)
    {
        return NotFound();
    }

    _context.Orders.Remove(Orders);
    await _context.SaveChangesAsync();

    return NoContent();
}

    private bool OrderExists(int id)
    {
        return _context.Orders.Any(e => e.OrderId == id);
    }

}
