using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICustomer.Data;
using Models;
using Controllers;
using Models.DTO;
using APIAddress.Services;
using DataAPI.Data;
using APICustomer.Services;
using DataAPI.Service;

namespace APICustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly DataAPIServices _apiService;
        private readonly CustomersService _service = new();

        public CustomersController(DataAPIContext context, CustomersService service, DataAPIServices dataAPIServices)
        {
            _context = context;
            _service = service;
            _apiService = dataAPIServices;
        }

        // GET: api/Customers
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer(int techType)
        {
            if (_context.Customer == null)
            {
                return NotFound();
            }
            List<Customer> customer = new List<Customer>();
            switch (techType)
            {
                case 0:
                    customer = await _context.Customer.Include(a => a.Address).ToListAsync();
                    break;
                case 1:
                    customer = await _service.GetAll(0);
                    break;
                case 2:
                    customer = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return customer;
        }

        // GET: api/Customers/5
        [HttpGet("{document},{techType}")]
        public async Task<ActionResult<Customer>> GetCustomer(string document, int techType)
        {
            if (_context.Customer == null)
            {
                return NotFound();
            }
            Customer? customer = new Customer();
            switch (techType)
            {
                case 0:
                    customer = await _context.Customer.Include(a => a.Address).SingleOrDefaultAsync(c => c.Document == document);
                    break;
                case 1:
                    customer = await _service.Get(document, 0);
                    break;
                case 2:
                    customer = await _service.Get(document, 1);
                    break;
                default:
                    return NotFound();
            }

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(string id, Customer customer)
        {
            if (id != customer.Document)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{techType}")]
        public async Task<ActionResult<Customer>> PostCustomer(int techType, CustomerDTO customerDTO)
        {
            if (_context.Customer == null)
            {
                return Problem("Entity set 'APICustomerContext.Customer'  is null.");
            }

            var customer = new Customer(customerDTO);
            var address = await _apiService.PostAddressAPI(customerDTO.Address); // Chama API Address
            if (address == null)
                return BadRequest("Endereço não encontrado");

            customer.Address = address;
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Address.Add(address);
                        _context.Customer.Add(customer);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        _service.Insert(customer, 0);
                        break;
                    case 2:
                        _service.Insert(customer, 1);
                        break;
                }
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.Document))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            
            return CreatedAtAction("GetCustomer", new { document = customer.Document, techType }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            if (_context.Customer == null)
            {
                return NotFound();
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(string id)
        {
            return (_context.Customer?.Any(e => e.Document == id)).GetValueOrDefault();
        }
    }
}
