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
using APIDriver.Services;
using Models.DTO;
using APIAddress.Services;

namespace APIDriver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductorsController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly ConductorsService _service = new();
        private readonly AddressesService _addressesService;

        public ConductorsController(DataAPIContext context, ConductorsService conductorsService, AddressesService addressesService)
        {
            _context = context;
            _service = conductorsService;
            _addressesService = addressesService;
        }

        // GET: api/Conductors
        [HttpGet("{techType}")]
        public async Task<ActionResult<List<Conductor>>> GetConductor(int techType)
        {
            if (_context.Conductor == null)
            {
                return NotFound();
            }
            List<Conductor>? conductor = new List<Conductor>();
            switch (techType)
            {
                case 0:
                    conductor = await _context.Conductor.ToListAsync();
                    break;
                case 1:
                    conductor = await _service.GetAll(0);
                    break;
                case 2:
                    conductor = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }

            if (conductor == null)
            {
                return NotFound();
            }

            return conductor;
        }

        // GET: api/Categories/5
        [HttpGet("{document},{techType}")]
        public async Task<ActionResult<Conductor>> GetConductor(string document, int techType)
        {
            if (_context.Conductor == null)
            {
                return NotFound();
            }
            Conductor? conductor = new Conductor();
            switch (techType)
            {
                case 0:
                    conductor = await _context.Conductor.FindAsync(document);
                    break;
                case 1:
                    conductor = await _service.Get(document, 0);
                    break;
                case 2:
                    conductor = await _service.Get(document, 1);
                    break;
                default:
                    return NotFound();
            }

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
        public async Task<ActionResult<Conductor>> PostConductor(ConductorDTO conductorDTO)
        {
            int techType = 0; // EF
            if (_context.Conductor == null)
            {
                return Problem("Entity set 'APIDriverContext.Conductor'  is null.");
            }

            Conductor conductor = new Conductor(conductorDTO);
            var driverLicense = _context.DriverLicense.Where(x => x.DriverId == conductorDTO.DriverLicenseId).FirstOrDefault();
            if (driverLicense == null)
                return BadRequest("CNH não existe");

            var address = await _addressesService.RetrieveAdressAPI(conductorDTO.Address); // usa API address
            if (address == null)
                return BadRequest("Endereço não encontrado");

            conductor.DriverLicense = driverLicense;
            conductor.Address = address;
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Conductor.Add(conductor);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        conductor.Address.Id = await _addressesService.Insert(address, 0);
                        conductor.Document = await _service.Insert(conductor, 0);
                        break;
                    case 2:
                        conductor.Address.Id = await _addressesService.Insert(address, 0);
                        conductor.Document = await _service.Insert(conductor, 1);
                        break;
                    default:
                        return NotFound();
                }
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
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConductor", new { id = conductor.Document, techType }, conductor);
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
