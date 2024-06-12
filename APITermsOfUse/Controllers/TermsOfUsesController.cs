using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITermsOfUse.Data;
using Models;
using DataAPI.Data;

namespace APITermsOfUse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermsOfUsesController : ControllerBase
    {
        private readonly DataAPIContext _context;

        public TermsOfUsesController(DataAPIContext context)
        {
            _context = context;
        }

        // GET: api/TermsOfUses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TermsOfUse>>> GetTermsOfUse()
        {
          if (_context.TermsOfUse == null)
          {
              return NotFound();
          }
            return await _context.TermsOfUse.ToListAsync();
        }

        // GET: api/TermsOfUses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TermsOfUse>> GetTermsOfUse(int id)
        {
          if (_context.TermsOfUse == null)
          {
              return NotFound();
          }
            var termsOfUse = await _context.TermsOfUse.FindAsync(id);

            if (termsOfUse == null)
            {
                return NotFound();
            }

            return termsOfUse;
        }

        // PUT: api/TermsOfUses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTermsOfUse(int id, TermsOfUse termsOfUse)
        {
            if (id != termsOfUse.Id)
            {
                return BadRequest();
            }

            _context.Entry(termsOfUse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TermsOfUseExists(id))
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

        // POST: api/TermsOfUses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TermsOfUse>> PostTermsOfUse(TermsOfUse termsOfUse)
        {
          if (_context.TermsOfUse == null)
          {
              return Problem("Entity set 'APITermsOfUseContext.TermsOfUse'  is null.");
          }
            _context.TermsOfUse.Add(termsOfUse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTermsOfUse", new { id = termsOfUse.Id }, termsOfUse);
        }

        // DELETE: api/TermsOfUses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTermsOfUse(int id)
        {
            if (_context.TermsOfUse == null)
            {
                return NotFound();
            }
            var termsOfUse = await _context.TermsOfUse.FindAsync(id);
            if (termsOfUse == null)
            {
                return NotFound();
            }

            _context.TermsOfUse.Remove(termsOfUse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TermsOfUseExists(int id)
        {
            return (_context.TermsOfUse?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
