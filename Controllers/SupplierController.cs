
using Northwind.Models;
using Northwind.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NorthWind.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    private readonly NorthwindContext _context;

    public SupplierController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Supplier>>> GetSupplier()
    {
        var Supplier = await _context.Suppliers
        .ToListAsync();
        return Supplier;
    }

    [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier Supplier)
        {
            _context.Suppliers.Add(Supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSupplier), new {id = Supplier}, Supplier);
        }

    [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var Supplier = await _context.Suppliers.FindAsync(id);

            if (Supplier == null)
            {
                return NotFound();
            }

            return Supplier;
        }

}