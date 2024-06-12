using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIFinancialPending.Data;
using Models;

namespace APIFinancialPending.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialPendingsController : ControllerBase
    {
        private readonly APIFinancialPendingContext _context;

        public FinancialPendingsController(APIFinancialPendingContext context)
        {
            _context = context;
        }

        // GET: api/FinancialPendings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialPending>>> GetFinancialPending()
        {
          if (_context.FinancialPending == null)
          {
              return NotFound();
          }
            return await _context.FinancialPending.ToListAsync();
        }

        // GET: api/FinancialPendings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialPending>> GetFinancialPending(int id)
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
        [HttpPost]
        public async Task<ActionResult<FinancialPending>> PostFinancialPending(FinancialPending financialPending)
        {
          if (_context.FinancialPending == null)
          {
              return Problem("Entity set 'APIFinancialPendingContext.FinancialPending'  is null.");
          }
            _context.FinancialPending.Add(financialPending);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinancialPending", new { id = financialPending.Id }, financialPending);
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
