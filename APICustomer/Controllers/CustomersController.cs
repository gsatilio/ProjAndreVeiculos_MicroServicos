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

namespace APICustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly AddressesService _addressesService;

        public CustomersController(DataAPIContext context, AddressesService addressesService)
        {
            _context = context;
            _addressesService = addressesService;
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
                    customer = await new CustomerController().GetAll(0);
                    break;
                case 2:
                    customer = await new CustomerController().GetAll(1);
                    break;
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
                    customer = await new CustomerController().Get(document, 0);
                    break;
                case 2:
                    customer = await new CustomerController().Get(document, 1);
                    break;
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
            var address = await _addressesService.RetrieveAdressAPI(customerDTO.Address);
            customer.Address = address;
            /*
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Customer.Add(customer);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        customer.Address.Id = new AddressController().Insert(address, 0);
                        new CustomerController().Insert(customer, 0);
                        break;
                    case 2:
                        customer.Address.Id = new AddressController().Insert(address, 1);
                        new CustomerController().Insert(customer, 1);
                        break;
                }
                _addressesService.InsertMongo(address);
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
            */
            return CreatedAtAction("GetCustomer", new { document = customer.Document, techType = 0 }, customer);
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
