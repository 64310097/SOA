
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Northwind.MySQL.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly NorthwindContext _context;

    public EmployeeController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
    {
        var Employees = await _context.Employees
            .ToListAsync();
       
        return Employees;
    }

    [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee Employee)
        {
            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new {id = Employee}, Employee);
        }

    [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var Employees = await _context.Employees.FindAsync(id);

            if (Employees == null)
            {
                return NotFound();
            }

            return Employees;
        }

}