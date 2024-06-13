using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIFinancialPending.Data;
using Models;
using DataAPI.Data;
using Models.DTO;
using APIFinancialPending.Services;

namespace APIFinancialPending.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialPendingsController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly FinancialPendingsService _service;

        public FinancialPendingsController(DataAPIContext context, FinancialPendingsService financialPendingsService)
        {
            _context = context;
            _service = financialPendingsService;
        }

        // GET: api/FinancialPendings
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<FinancialPending>>> GetFinancialPending(int techType)
        {
            if (_context.FinancialPending == null)
            {
                return NotFound();
            }
            List<FinancialPending> financialPending = new List<FinancialPending>();
            switch (techType)
            {
                case 0:
                    financialPending = await _context.FinancialPending.ToListAsync();
                    break;
                case 1:
                    financialPending = await _service.GetAll(0);
                    break;
                case 2:
                    financialPending = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return financialPending;
        }

        // GET: api/FinancialPendings/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<FinancialPending>> GetFinancialPending(int id, int techType)
        {
            if (_context.FinancialPending == null)
            {
                return NotFound();
            }
            FinancialPending? financialPending = new FinancialPending();
            switch (techType)
            {
                case 0:
                    financialPending = await _context.FinancialPending.FindAsync(id);
                    break;
                case 1:
                    financialPending = await _service.Get(id, 0);
                    break;
                case 2:
                    financialPending = await _service.Get(id, 1);
                    break;
                default:
                    return NotFound();
            }

            if (financialPending == null)
            {
                return NotFound();
            }

            return financialPending;
        }

        // PUT: api/FinancialPendings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinancialPending(int id, FinancialPending financialPending)
        {
            if (id != financialPending.Id)
            {
                return BadRequest();
            }

            _context.Entry(financialPending).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialPendingExists(id))
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

        // POST: api/FinancialPendings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{techType}")]
        public async Task<ActionResult<FinancialPending>> PostFinancialPending(int techType, FinancialPendingDTO financialPendingDTO)
        {
            if (_context.FinancialPending == null)
            {
                return Problem("Entity set 'APIFinancialPendingContext.FinancialPending'  is null.");
            }
            var financialPending = new FinancialPending(financialPendingDTO);

            var customer = _context.Customer.Where(x => x.Document == financialPendingDTO.Document).Include(c => c.Address).FirstOrDefault();
            if (customer == null)
                return BadRequest("Cliente não existente");

            financialPending.Customer = customer;
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.FinancialPending.Add(financialPending);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        financialPending.Id = await _service.Insert(financialPending, 0);
                        break;
                    case 2:
                        financialPending.Id = await _service.Insert(financialPending, 1);
                        break;
                }
            }
            catch (DbUpdateException)
            {
                if (FinancialPendingExists(financialPending.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFinancialPending", new { document = financialPending.Id, techType = techType }, financialPending);
        }

        // DELETE: api/FinancialPendings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancialPending(int id)
        {
            if (_context.FinancialPending == null)
            {
                return NotFound();
            }
            var financialPending = await _context.FinancialPending.FindAsync(id);
            if (financialPending == null)
            {
                return NotFound();
            }

            _context.FinancialPending.Remove(financialPending);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinancialPendingExists(int id)
        {
            return (_context.FinancialPending?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
