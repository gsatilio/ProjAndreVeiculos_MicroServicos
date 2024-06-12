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
    public class TermsOfUseAgreementsController : ControllerBase
    {
        private readonly DataAPIContext _context;

        public TermsOfUseAgreementsController(DataAPIContext context)
        {
            _context = context;
        }

        // GET: api/TermsOfUseAgreements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TermsOfUseAgreement>>> GetTermsOfUseAgreement()
        {
          if (_context.TermsOfUseAgreement == null)
          {
              return NotFound();
          }
            return await _context.TermsOfUseAgreement.ToListAsync();
        }

        // GET: api/TermsOfUseAgreements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TermsOfUseAgreement>> GetTermsOfUseAgreement(int id)
        {
          if (_context.TermsOfUseAgreement == null)
          {
              return NotFound();
          }
            var termsOfUseAgreement = await _context.TermsOfUseAgreement.FindAsync(id);

            if (termsOfUseAgreement == null)
            {
                return NotFound();
            }

            return termsOfUseAgreement;
        }

        // PUT: api/TermsOfUseAgreements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTermsOfUseAgreement(int id, TermsOfUseAgreement termsOfUseAgreement)
        {
            if (id != termsOfUseAgreement.Id)
            {
                return BadRequest();
            }

            _context.Entry(termsOfUseAgreement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TermsOfUseAgreementExists(id))
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

        // POST: api/TermsOfUseAgreements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TermsOfUseAgreement>> PostTermsOfUseAgreement(TermsOfUseAgreement termsOfUseAgreement)
        {
          if (_context.TermsOfUseAgreement == null)
          {
              return Problem("Entity set 'APITermsOfUseContext.TermsOfUseAgreement'  is null.");
          }
            _context.TermsOfUseAgreement.Add(termsOfUseAgreement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTermsOfUseAgreement", new { id = termsOfUseAgreement.Id }, termsOfUseAgreement);
        }

        // DELETE: api/TermsOfUseAgreements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTermsOfUseAgreement(int id)
        {
            if (_context.TermsOfUseAgreement == null)
            {
                return NotFound();
            }
            var termsOfUseAgreement = await _context.TermsOfUseAgreement.FindAsync(id);
            if (termsOfUseAgreement == null)
            {
                return NotFound();
            }

            _context.TermsOfUseAgreement.Remove(termsOfUseAgreement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TermsOfUseAgreementExists(int id)
        {
            return (_context.TermsOfUseAgreement?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
