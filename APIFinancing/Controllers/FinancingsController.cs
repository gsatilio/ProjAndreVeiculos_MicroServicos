using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIFinancing.Data;
using Models;
using DataAPI.Data;
using Models.DTO;
using APIFinancing.Services;
using APISale.Services;
using Services;
using APIBank.Services;

namespace APIFinancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancingsController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly FinancingsService _service;
        private readonly BanksService _bank;
        private readonly SalesService _sale;

        public FinancingsController(DataAPIContext context, FinancingsService financingsService, BanksService banksService, SalesService salesService )
        {
            _context = context;
            _service = financingsService;
            _bank = banksService;
            _sale = salesService;
        }

        // GET: api/Financing
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Financing>>> GetFinancing(int techType)
        {
            if (_context.Financing == null)
            {
                return NotFound();
            }
            List<Financing> financing = new List<Financing>();
            switch (techType)
            {
                case 0:
                    financing = await _context.Financing
                        .Include(p => p.Bank)
                        .Include(p => p.Sale)
                        .Include(p => p.Sale.Customer)
                        .Include(p => p.Sale.Car)
                        .Include(p => p.Sale.Customer.Address)
                        .Include(p => p.Sale.Employee)
                        .Include(p => p.Sale.Employee.Address)
                        .Include(p => p.Sale.Employee.Role)
                        .Include(p => p.Sale.Payment.CreditCard)
                        .Include(p => p.Sale.Payment.Boleto)
                        .Include(p => p.Sale.Payment.Pix)
                        .Include(p => p.Sale.Payment.Pix.PixType)
                        .ToListAsync();
                    break;
                case 1:
                    financing = await _service.GetAll(0);
                    break;
                case 2:
                    financing = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return financing;
        }

        // GET: api/Financing/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Financing>> GetFinancing(int id, int techType)
        {
            if (_context.Financing == null)
            {
                return NotFound();
            }

            Financing? financing = new Financing();
            switch (techType)
            {
                case 0:
                    financing = await _context.Financing
                        .Include(p => p.Bank)
                        .Include(p => p.Sale)
                        .Include(p => p.Sale.Customer)
                        .Include(p => p.Sale.Car)
                        .Include(p => p.Sale.Customer.Address)
                        .Include(p => p.Sale.Employee)
                        .Include(p => p.Sale.Employee.Address)
                        .Include(p => p.Sale.Employee.Role)
                        .Include(p => p.Sale.Payment.CreditCard)
                        .Include(p => p.Sale.Payment.Boleto)
                        .Include(p => p.Sale.Payment.Pix)
                        .Include(p => p.Sale.Payment.Pix.PixType)
                        .SingleOrDefaultAsync(p => p.Id == id);
                    break;
                case 1:
                    financing = await _service.Get(id, 0);
                    break;
                case 2:
                    financing = await _service.Get(id, 1);
                    break;
                default:
                    return NotFound();
            }

            if (financing == null)
            {
                return NotFound();
            }

            return financing;
        }

        // PUT: api/Financings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinancing(int id, Financing financing)
        {
            if (id != financing.Id)
            {
                return BadRequest();
            }

            _context.Entry(financing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancingExists(id))
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

        // POST: api/Financings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Financing>> PostFinancing(FinancingDTO financingDTO)
        {
            int techType = 2; // Dapper
            if (_context.Financing == null)
            {
                return Problem("Entity set 'APIFinancingContext.Financing'  is null.");
            }
            var financing = new Financing(financingDTO);
            var sale = await _sale.Get(financingDTO.SaleId, techType);
            var bank = _bank.GetMongoById(financingDTO.BankCNPJ);

            if (sale == null)
                return BadRequest("Venda não existe");

            if (bank == null)
                return BadRequest("Banco não existe");

            financing.Sale = sale;
            financing.Bank = bank;
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Financing.Add(financing);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        financing.Id = await _service.Insert(financing, 0);
                        break;
                    case 2:
                        financing.Id = await _service.Insert(financing, 1);
                        break;
                }
            }
            catch (DbUpdateException)
            {
                if (FinancingExists(financing.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFinancing", new { document = financing.Id, techType }, financing);
        }

        // DELETE: api/Financings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancing(int id)
        {
            if (_context.Financing == null)
            {
                return NotFound();
            }
            var financing = await _context.Financing.FindAsync(id);
            if (financing == null)
            {
                return NotFound();
            }

            _context.Financing.Remove(financing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinancingExists(int id)
        {
            return (_context.Financing?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
