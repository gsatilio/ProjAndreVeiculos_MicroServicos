using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDriver.Data;
using Models;
using Models.DTO;
using DataAPI.Data;

namespace APIDriver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverLicensesController : ControllerBase
    {
        private readonly DataAPIContext _context;

        private readonly CategoriesController _categoriesController;
        private readonly ConductorsController _conductorsController;

        public DriverLicensesController(DataAPIContext context)
        {
            _context = context;
        }

        // GET: api/DriverLicenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverLicense>>> GetDriverLicense()
        {
            if (_context.DriverLicense == null)
            {
                return NotFound();
            }
            return await _context.DriverLicense.ToListAsync();
        }

        // GET: api/DriverLicenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverLicense>> GetDriverLicense(long id)
        {
            if (_context.DriverLicense == null)
            {
                return NotFound();
            }
            var driverLicense = await _context.DriverLicense.FindAsync(id);

            if (driverLicense == null)
            {
                return NotFound();
            }

            return driverLicense;
        }

        // PUT: api/DriverLicenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriverLicense(long id, DriverLicense driverLicense)
        {
            if (id != driverLicense.DriverId)
            {
                return BadRequest();
            }

            _context.Entry(driverLicense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverLicenseExists(id))
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

        // POST: api/DriverLicenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DriverLicense>> PostDriverLicense(DriverLicenseDTO driverLicenseDTO)
        {
            if (_context.DriverLicense == null)
            {
                return Problem("Entity set 'APIDriverContext.DriverLicense'  is null.");
            }
            DriverLicense driverLicense = new(driverLicenseDTO);
            var category = _context.Category.Find(driverLicenseDTO.Category.Id);
            if (category != null)
            {
                driverLicense.Category = category;
                _context.DriverLicense.Add(driverLicense);
                await _context.SaveChangesAsync();
            } else
            {
                return BadRequest("Categoria inexistente");
            }


            return CreatedAtAction("GetDriverLicense", new { id = driverLicense.DriverId }, driverLicense);
        }

        // DELETE: api/DriverLicenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriverLicense(long id)
        {
            if (_context.DriverLicense == null)
            {
                return NotFound();
            }
            var driverLicense = await _context.DriverLicense.FindAsync(id);
            if (driverLicense == null)
            {
                return NotFound();
            }

            _context.DriverLicense.Remove(driverLicense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriverLicenseExists(long id)
        {
            return (_context.DriverLicense?.Any(e => e.DriverId == id)).GetValueOrDefault();
        }
    }
}
