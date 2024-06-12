using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDriver.Data;
using Models;
using DataAPI.Data;

namespace APIDriver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductorsController : ControllerBase
    {
        private readonly DataAPIContext _context;

        public ConductorsController(DataAPIContext context)
        {
            _context = context;
        }

        // GET: api/Conductors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conductor>>> GetConductor()
        {
          if (_context.Conductor == null)
          {
              return NotFound();
          }
            return await _context.Conductor.ToListAsync();
        }

        // GET: api/Conductors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conductor>> GetConductor(string id)
        {
          if (_context.Conductor == null)
          {
              return NotFound();
          }
            var conductor = await _context.Conductor.FindAsync(id);

            if (conductor == null)
            {
                return NotFound();
            }

            return conductor;
        }

        // PUT: api/Conductors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConductor(string id, Conductor conductor)
        {
            if (id != conductor.Document)
            {
                return BadRequest();
            }

            _context.Entry(conductor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConductorExists(id))
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

        // POST: api/Conductors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conductor>> PostConductor(Conductor conductor)
        {
          if (_context.Conductor == null)
          {
              return Problem("Entity set 'APIDriverContext.Conductor'  is null.");
          }
            _context.Conductor.Add(conductor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ConductorExists(conductor.Document))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetConductor", new { id = conductor.Document }, conductor);
        }

        // DELETE: api/Conductors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConductor(string id)
        {
            if (_context.Conductor == null)
            {
                return NotFound();
            }
            var conductor = await _context.Conductor.FindAsync(id);
            if (conductor == null)
            {
                return NotFound();
            }

            _context.Conductor.Remove(conductor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConductorExists(string id)
        {
            return (_context.Conductor?.Any(e => e.Document == id)).GetValueOrDefault();
        }
    }
}
