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

namespace APISale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly APISaleContext _context;

        public SalesController(APISaleContext context)
        {
            _context = context;
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
                    addresses = await _context.Sale.Include(p => p.Car).Include(p => p.Customer).Include(p => p.Customer.Address).Include(p => p.Employee).Include(p => p.Employee.Address).Include(p => p.Payment.CreditCard).Include(p => p.Payment.Boleto).Include(p => p.Payment.Pix).Include(p => p.Payment.Pix.PixType).ToListAsync();
                    break;
                case 1:
                    addresses = await new SaleController().GetAll(0);
                    break;
                case 2:
                    addresses = await new SaleController().GetAll(1);
                    break;
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
                    address = await _context.Sale.Include(p => p.Car).Include(p => p.Customer).Include(p => p.Customer.Address).Include(p => p.Employee).Include(p => p.Employee.Address).Include(p => p.Payment.CreditCard).Include(p => p.Payment.Boleto).Include(p => p.Payment.Pix).Include(p => p.Payment.Pix.PixType).SingleOrDefaultAsync(p => p.Id == id);
                    break;
                case 1:
                    address = await new SaleController().Get(id, 0);
                    break;
                case 2:
                    address = await new SaleController().Get(id, 1);
                    break;
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
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
          if (_context.Sale == null)
          {
              return Problem("Entity set 'APISaleContext.Sale'  is null.");
          }
            _context.Sale.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
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
