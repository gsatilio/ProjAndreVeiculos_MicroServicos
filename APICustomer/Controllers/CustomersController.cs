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

namespace APICustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly APICustomerContext _context;

        public CustomersController(APICustomerContext context)
        {
            _context = context;
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
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDTO customerDTO)
        {
            if (_context.Customer == null)
            {
                return Problem("Entity set 'APICustomerContext.Customer'  is null.");
            }

            Customer customer = new();
            Address address = new();
            AddressesService addressesService = new AddressesService();
            address.CEP = customerDTO.Address.CEP;
            address = await addressesService.RetrieveAdressAPI(address);
            address.Complement = customerDTO.Address.Complement;
            address.Number = customerDTO.Address.Number;
            address.StreetType = customerDTO.Address.StreetType;
            customer.Name = customerDTO.Name;
            customer.Document = customerDTO.Document;
            customer.DateOfBirth = customerDTO.DateOfBirth;
            customer.Income = customerDTO.Income;
            customer.PDFDocument = customerDTO.PDFDocument;
            customer.Address = address;
            customer.Phone = customerDTO.Phone;
            customer.Email = customerDTO.Email;
            _context.Customer.Add(customer);
            try
            {
                await _context.SaveChangesAsync();
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
            // alterado parametros para retorno
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
