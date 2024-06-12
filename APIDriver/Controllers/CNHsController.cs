using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIConductor.Data;
using Models;
using Models.DTO;
using APIAddress.Services;
using Controllers;
using System.Net;

namespace APIConductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CNHsController : ControllerBase
    {
        private readonly APIConductorContext _context;

        public CNHsController(APIConductorContext context)
        {
            _context = context;
        }

        // GET: api/CNHs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CNH>>> GetCNH()
        {
            if (_context.CNH == null)
            {
                return NotFound();
            }
            return await _context.CNH.ToListAsync();
        }

        // GET: api/CNHs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CNH>> GetCNH(long id)
        {
            if (_context.CNH == null)
            {
                return NotFound();
            }
            var cNH = await _context.CNH.FindAsync(id);

            if (cNH == null)
            {
                return NotFound();
            }

            return cNH;
        }

        // PUT: api/CNHs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCNH(long id, CNH cNH)
        {
            if (id != cNH.DriverLicense)
            {
                return BadRequest();
            }

            _context.Entry(cNH).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CNHExists(id))
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

        // POST: api/CNHs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("techType")]
        public async Task<ActionResult<CNH>> PostCNH(int techType, CNHDTO cnhDTO)
        {
            if (_context.CNH == null)
            {
                return Problem("Entity set 'APIConductorContext.CNH'  is null.");
            }
            CategoryController categoryController = new CategoryController();
            var category = await categoryController.Get(cnhDTO.Category.Id, 0);
            if (category == null)
                return BadRequest("Categoria não existe");

            var cnh = new CNH(cnhDTO);
            cnh.Category = category;
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.CNH.Add(cnh);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        new CNHController().Insert(cnh, 0);
                        break;
                    case 2:
                        new CNHController().Insert(cnh, 1);
                        break;
                }
            }
            catch (DbUpdateException)
            {
                if (CNHExists(cnh.DriverLicense))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetCNH", new { id = cnh.DriverLicense }, cnh);
        }

        // DELETE: api/CNHs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCNH(long id)
        {
            if (_context.CNH == null)
            {
                return NotFound();
            }
            var cNH = await _context.CNH.FindAsync(id);
            if (cNH == null)
            {
                return NotFound();
            }

            _context.CNH.Remove(cNH);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CNHExists(long id)
        {
            return (_context.CNH?.Any(e => e.DriverLicense == id)).GetValueOrDefault();
        }
    }
}
