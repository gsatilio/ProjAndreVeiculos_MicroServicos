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
using APIDriver.Services;

namespace APIDriver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverLicensesController : ControllerBase
    {
        private readonly DataAPIContext _context;

        private readonly DriverLicensesService _service = new();

        public DriverLicensesController(DataAPIContext context, DriverLicensesService driverLicensesService )
        {
            _context = context;
            _service = driverLicensesService;
        }

        // GET: api/Categories/5
        [HttpGet("{techType}")]
        public async Task<ActionResult<List<DriverLicense>>> GetDriverLicense(int techType)
        {
            if (_context.DriverLicense == null)
            {
                return NotFound();
            }
            List<DriverLicense>? driverLicense = new List<DriverLicense>();
            switch (techType)
            {
                case 0:
                    driverLicense = await _context.DriverLicense.ToListAsync();
                    break;
                case 1:
                    driverLicense = await _service.GetAll(0);
                    break;
                case 2:
                    driverLicense = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }

            if (driverLicense == null)
            {
                return NotFound();
            }

            return driverLicense;
        }

        // GET: api/Categories/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<DriverLicense>> GetDriverLicense(long id, int techType)
        {
            if (_context.DriverLicense == null)
            {
                return NotFound();
            }
            DriverLicense? driverLicense = new DriverLicense();
            switch (techType)
            {
                case 0:
                    driverLicense = await _context.DriverLicense.FindAsync(id);
                    break;
                case 1:
                    driverLicense = await _service.Get(id, 0);
                    break;
                case 2:
                    driverLicense = await _service.Get(id, 1);
                    break;
                default:
                    return NotFound();
            }

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
        [HttpPost("{techType}")]
        public async Task<ActionResult<DriverLicense>> PostDriverLicense(int techType, DriverLicenseDTO driverLicenseDTO)
        {
            if (_context.DriverLicense == null)
            {
                return Problem("Entity set 'APIDriverContext.DriverLicense'  is null.");
            }

            DriverLicense driverLicense = new DriverLicense(driverLicenseDTO);
            var category = _context.Category.Where(x => x.Id == driverLicenseDTO.Category.Id).FirstOrDefault();
            if (category == null)
                return BadRequest("Categoria não existe");

            driverLicense.Category = category;
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.DriverLicense.Add(driverLicense);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        driverLicense.DriverId = await _service.Insert(driverLicense, 0);
                        break;
                    case 2:
                        driverLicense.DriverId = await _service.Insert(driverLicense, 1);
                        break;
                    default:
                        return NotFound();
                }
            }
            catch (DbUpdateException)
            {
                if (DriverLicenseExists(driverLicense.DriverId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriverLicense", new { id = driverLicense.DriverId, techType }, driverLicense);
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
