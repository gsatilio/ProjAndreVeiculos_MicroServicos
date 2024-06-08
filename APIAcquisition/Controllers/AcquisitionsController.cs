using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIAcquisition.Data;
using Models;

namespace APIAcquisition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcquisitionsController : ControllerBase
    {
        private readonly APIAcquisitionContext _context;

        public AcquisitionsController(APIAcquisitionContext context)
        {
            _context = context;
        }

        // GET: api/Acquisitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Acquisition>>> GetAcquisition()
        {
          if (_context.Acquisition == null)
          {
              return NotFound();
          }
            return await _context.Acquisition.ToListAsync();
        }

        // GET: api/Acquisitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Acquisition>> GetAcquisition(int id)
        {
          if (_context.Acquisition == null)
          {
              return NotFound();
          }
            var acquisition = await _context.Acquisition.FindAsync(id);

            if (acquisition == null)
            {
                return NotFound();
            }

            return acquisition;
        }

        // PUT: api/Acquisitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcquisition(int id, Acquisition acquisition)
        {
            if (id != acquisition.Id)
            {
                return BadRequest();
            }

            _context.Entry(acquisition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcquisitionExists(id))
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

        // POST: api/Acquisitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Acquisition>> PostAcquisition(Acquisition acquisition)
        {
          if (_context.Acquisition == null)
          {
              return Problem("Entity set 'APIAcquisitionContext.Acquisition'  is null.");
          }
            _context.Acquisition.Add(acquisition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAcquisition", new { id = acquisition.Id }, acquisition);
        }

        // DELETE: api/Acquisitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcquisition(int id)
        {
            if (_context.Acquisition == null)
            {
                return NotFound();
            }
            var acquisition = await _context.Acquisition.FindAsync(id);
            if (acquisition == null)
            {
                return NotFound();
            }

            _context.Acquisition.Remove(acquisition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AcquisitionExists(int id)
        {
            return (_context.Acquisition?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
