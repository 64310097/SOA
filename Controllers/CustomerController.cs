
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Northwind.MySQL.Controllers;



[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly NorthwindContext _context;

    public CustomerController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        var Customers = await _context.Customers
            .ToListAsync();

        return Customers;
    }

    [HttpGet("id")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        var Customer = await _context.Customers.FindAsync(id);
        if (Customer == null)
        {
            return NotFound();
        }
        return Customer;
    }

      [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer Customers)
        {
            _context.Customers.Add(Customers);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new {id = Customers}, Customers);
        }
   [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(String id,Customer Customer)
        {
            if (id != Customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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
public async Task<ActionResult> DeleteCustomer(int id)
{
    var Customers = await _context.Customers.FindAsync(id);
    if (Customers == null)
    {
        return NotFound();
    }

    _context.Customers.Remove(Customers);
    await _context.SaveChangesAsync();

    return NoContent();
}

    private bool CustomerExists(string id)
    {
        return _context.Customers.Any(e => e.CustomerId == id);
    }

}
