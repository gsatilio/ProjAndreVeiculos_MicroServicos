using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDependent.Data;
using Models;
using DataAPI.Data;

namespace APIDependent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependentsController : ControllerBase
    {
        private readonly DataAPIContext _context;

        public DependentsController(DataAPIContext context)
        {
            _context = context;
        }

        // GET: api/Dependents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dependent>>> GetDependent()
        {
          if (_context.Dependent == null)
          {
              return NotFound();
          }
            return await _context.Dependent.ToListAsync();
        }

        // GET: api/Dependents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dependent>> GetDependent(string id)
        {
          if (_context.Dependent == null)
          {
              return NotFound();
          }
            var dependent = await _context.Dependent.FindAsync(id);

            if (dependent == null)
            {
                return NotFound();
            }

            return dependent;
        }

        // PUT: api/Dependents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDependent(string id, Dependent dependent)
        {
            if (id != dependent.Document)
            {
                return BadRequest();
            }

            _context.Entry(dependent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DependentExists(id))
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

        // POST: api/Dependents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dependent>> PostDependent(Dependent dependent)
        {
          if (_context.Dependent == null)
          {
              return Problem("Entity set 'APIDependentContext.Dependent'  is null.");
          }
            _context.Dependent.Add(dependent);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DependentExists(dependent.Document))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDependent", new { id = dependent.Document }, dependent);
        }

        // DELETE: api/Dependents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDependent(string id)
        {
            if (_context.Dependent == null)
            {
                return NotFound();
            }
            var dependent = await _context.Dependent.FindAsync(id);
            if (dependent == null)
            {
                return NotFound();
            }

            _context.Dependent.Remove(dependent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DependentExists(string id)
        {
            return (_context.Dependent?.Any(e => e.Document == id)).GetValueOrDefault();
        }
    }
}
