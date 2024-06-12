using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPix.Data;
using Models;
using Controllers;
using DataAPI.Data;

namespace APIPix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PixesController : ControllerBase
    {
        private readonly DataAPIContext _context;

        public PixesController(DataAPIContext context)
        {
            _context = context;
        }

        // GET: api/Pix
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Pix>>> GetPix(int techType)
        {
            if (_context.Pix == null)
            {
                return NotFound();
            }
            List<Pix> customer = new List<Pix>();
            switch (techType)
            {
                case 0:
                    customer = await _context.Pix.Include(a => a.PixType).ToListAsync();
                    break;
                case 1:
                    customer = await new PixController().GetAll(0);
                    break;
                case 2:
                    customer = await new PixController().GetAll(1);
                    break;
            }
            return customer;
        }

        // GET: api/Pixs/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Pix>> GetPix(int id, int techType)
        {
            if (_context.Pix == null)
            {
                return NotFound();
            }
            Pix? customer = new Pix();
            switch (techType)
            {
                case 0:
                    customer = await _context.Pix.Include(a => a.PixType).SingleOrDefaultAsync(c => c.Id == id);
                    break;
                case 1:
                    customer = await new PixController().Get(id, 0);
                    break;
                case 2:
                    customer = await new PixController().Get(id, 1);
                    break;
            }

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Pixes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPix(int id, Pix pix)
        {
            if (id != pix.Id)
            {
                return BadRequest();
            }

            _context.Entry(pix).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PixExists(id))
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

        // POST: api/Pixes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pix>> PostPix(Pix pix)
        {
            if (_context.Pix == null)
            {
                return Problem("Entity set 'APIPixContext.Pix'  is null.");
            }
            _context.Pix.Add(pix);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPix", new { id = pix.Id }, pix);
        }

        // DELETE: api/Pixes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePix(int id)
        {
            if (_context.Pix == null)
            {
                return NotFound();
            }
            var pix = await _context.Pix.FindAsync(id);
            if (pix == null)
            {
                return NotFound();
            }

            _context.Pix.Remove(pix);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PixExists(int id)
        {
            return (_context.Pix?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
