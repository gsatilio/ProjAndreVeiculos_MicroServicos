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

namespace APIFinancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancingsController : ControllerBase
    {
        private readonly DataAPIContext _context;

        public FinancingsController(DataAPIContext context)
        {
            _context = context;
        }

        // GET: api/Financings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Financing>>> GetFinancing()
        {
          if (_context.Financing == null)
          {
              return NotFound();
          }
            return await _context.Financing.ToListAsync();
        }

        // GET: api/Financings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Financing>> GetFinancing(int id)
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
        public async Task<ActionResult<Financing>> PostFinancing(Financing financing)
        {
          if (_context.Financing == null)
          {
              return Problem("Entity set 'APIFinancingContext.Financing'  is null.");
          }
            _context.Financing.Add(financing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinancing", new { id = financing.Id }, financing);
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
