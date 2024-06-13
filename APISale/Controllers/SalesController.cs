using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APISale.Data;
using Models;
using Controllers;
using DataAPI.Data;
using APISale.Services;
using Models.DTO;

namespace APISale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly SalesService _service = new();

        public SalesController(DataAPIContext context, SalesService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Sale
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSale(int techType)
        {
            if (_context.Sale == null)
            {
                return NotFound();
            }
            List<Sale> addresses = new List<Sale>();
            switch (techType)
            {
                case 0:
                    addresses = await _context.Sale
                        .Include(p => p.Car)
                        .Include(p => p.Customer)
                        .Include(p => p.Customer.Address)
                        .Include(p => p.Employee)
                        .Include(p => p.Employee.Role)
                        .Include(p => p.Employee.Address)
                        .Include(p => p.Payment.CreditCard)
                        .Include(p => p.Payment.Boleto)
                        .Include(p => p.Payment.Pix)
                        .Include(p => p.Payment.Pix.PixType).ToListAsync();
                    break;
                case 1:
                    addresses = await _service.GetAll(0);
                    break;
                case 2:
                    addresses = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return addresses;
        }

        // GET: api/Sale/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Sale>> GetSale(int id, int techType)
        {
            if (_context.Sale == null)
            {
                return NotFound();
            }

            Sale? address = new Sale();
            switch (techType)
            {
                case 0:
                    address = await _context.Sale
                        .Include(p => p.Car)
                        .Include(p => p.Customer)
                        .Include(p => p.Customer.Address)
                        .Include(p => p.Employee)
                        .Include(p => p.Employee.Role)
                        .Include(p => p.Employee.Address)
                        .Include(p => p.Payment.CreditCard)
                        .Include(p => p.Payment.Boleto)
                        .Include(p => p.Payment.Pix)
                        .Include(p => p.Payment.Pix.PixType)
                        .SingleOrDefaultAsync(p => p.Id == id);
                    break;
                case 1:
                    address = await _service.Get(id, 0);
                    break;
                case 2:
                    address = await _service.Get(id, 1);
                    break;
                default:
                    return NotFound();
            }

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, Sale sale)
        {
            if (id != sale.Id)
            {
                return BadRequest();
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
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

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(SaleDTO saleDTO)
        {
            if (_context.Sale == null)
            {
                return Problem("Entity set 'APISaleContext.Sale'  is null.");
            }

            Sale sale = new(saleDTO);
            var car = _context.Car.Where(c => c.LicensePlate == saleDTO.LicensePlate).FirstOrDefault();
            var customer = _context.Customer.Where(c => c.Document == saleDTO.CustomerDocument).FirstOrDefault();
            var employee = _context.Employee.Where(c => c.Document == saleDTO.EmployeeDocument).FirstOrDefault();
            var payment = _context.Payment.Where(c => c.Id == saleDTO.IdPayment).FirstOrDefault();

            if (car == null)
                return BadRequest("Carro não encontrado");
            if (customer == null)
                return BadRequest("Cliente não encontrado");
            if (employee == null)
                return BadRequest("Funcionario não encontrado");
            if (payment == null)
                return BadRequest("Pagamento não encontrado");

            sale.Car = car;
            sale.Customer = customer;
            sale.Employee = employee;
            sale.Payment = payment;

            _context.Sale.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.Id, techType = 0 }, sale);
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            if (_context.Sale == null)
            {
                return NotFound();
            }
            var sale = await _context.Sale.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sale.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleExists(int id)
        {
            return (_context.Sale?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
