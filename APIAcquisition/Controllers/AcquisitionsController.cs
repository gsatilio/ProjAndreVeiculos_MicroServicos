using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIAcquisition.Data;
using Models;
using APIAcquisition.Services;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Models.DTO;

namespace APIAcquisition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcquisitionsController : ControllerBase
    {
        private readonly APIAcquisitionContext _context;
        private readonly AcquisitionService _service;

        public AcquisitionsController(APIAcquisitionContext context, AcquisitionService acquisitionService)
        {
            _context = context;
            _service = acquisitionService;
        }

        // GET: api/Acquisitions
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Acquisition>>> GetAcquisition(int techType)
        {
            if (_context.Acquisition == null)
            {
                return NotFound();
            }
            List<Acquisition> acquisition = new List<Acquisition>();
            switch (techType)
            {
                case 0:
                    acquisition = await _context.Acquisition.Include(c => c.Car).ToListAsync();
                    break;
                case 1:
                    acquisition = await _service.GetAll(0);
                    break;
                case 2:
                    acquisition = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return acquisition;
        }

        // GET: api/Acquisitions/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Acquisition>> GetAcquisition(int id, int techType)
        {
            if (_context.Acquisition == null)
            {
                return NotFound();
            }
            Acquisition? acquisition = new Acquisition();
            switch (techType)
            {
                case 0:
                    acquisition = await _context.Acquisition.Include(c => c.Car).SingleOrDefaultAsync(c => c.Id == id);
                    break;
                case 1:
                    acquisition = await _service.Get(id, 0);
                    break;
                case 2:
                    acquisition = await _service.Get(id, 1);
                    break;
                default:
                    return NotFound();
            }

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
        [HttpPost("{techType}")]
        public async Task<ActionResult<Acquisition>> PostAcquisition(Acquisition acquisition, int techType)
        {
            if (_context.Acquisition == null)
            {
                return Problem("Entity set 'APIAcquisitionContext.Acquisition'  is null.");
            }

            var car = _context.Car.Where(x => x.LicensePlate == acquisition.Car.LicensePlate).FirstOrDefault();
            if (car == null)
                return BadRequest("Carro nao localizado.");

            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Acquisition.Add(acquisition);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        acquisition.Id = await _service.Insert(acquisition, 0);
                        acquisition.Car = car;
                        break;
                    case 2:
                        acquisition.Id = await _service.Insert(acquisition, 1);
                        acquisition.Car = car;
                        break;
                    default:
                        return NotFound();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return CreatedAtAction("GetAcquisition", new { id = acquisition.Id, techType }, acquisition);
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
