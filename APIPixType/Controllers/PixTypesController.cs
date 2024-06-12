using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPixType.Data;
using Models;
using Controllers;
using DataAPI.Data;

namespace APIPixType.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PixTypesController : ControllerBase
    {
        private readonly DataAPIContext _context;

        public PixTypesController(DataAPIContext context)
        {
            _context = context;
        }

        // GET: api/PixType
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<PixType>>> GetPixType(int techType)
        {
            if (_context.PixType == null)
            {
                return NotFound();
            }
            List<PixType> addresses = new List<PixType>();
            switch (techType)
            {
                case 0:
                    addresses = await _context.PixType.ToListAsync();
                    break;
                case 1:
                    addresses = await new PixTypeController().GetAll(0);
                    break;
                case 2:
                    addresses = await new PixTypeController().GetAll(1);
                    break;
            }
            return addresses;
        }

        // GET: api/PixType/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<PixType>> GetPixType(int id, int techType)
        {
            if (_context.PixType == null)
            {
                return NotFound();
            }

            PixType? address = new PixType();
            switch (techType)
            {
                case 0:
                    address = await _context.PixType.FindAsync(id);
                    break;
                case 1:
                    address = await new PixTypeController().Get(id, 0);
                    break;
                case 2:
                    address = await new PixTypeController().Get(id, 1);
                    break;
            }

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/PixTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPixType(int id, PixType pixType)
        {
            if (id != pixType.Id)
            {
                return BadRequest();
            }

            _context.Entry(pixType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PixTypeExists(id))
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

        // POST: api/PixTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PixType>> PostPixType(PixType pixType)
        {
          if (_context.PixType == null)
          {
              return Problem("Entity set 'APIPixTypeContext.PixType'  is null.");
          }
            _context.PixType.Add(pixType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPixType", new { id = pixType.Id }, pixType);
        }

        // DELETE: api/PixTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePixType(int id)
        {
            if (_context.PixType == null)
            {
                return NotFound();
            }
            var pixType = await _context.PixType.FindAsync(id);
            if (pixType == null)
            {
                return NotFound();
            }

            _context.PixType.Remove(pixType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PixTypeExists(int id)
        {
            return (_context.PixType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
