using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIConductor.Data;
using Models;
using Controllers;
using System.Net;
using Models.DTO;
using APIAddress.Services;

namespace APIConductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductorsController : ControllerBase
    {
        private readonly APIConductorContext _context;
        private readonly AddressesService _addressesService;

        public ConductorsController(APIConductorContext context)
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
        [HttpPost("techType")]
        public async Task<ActionResult<Conductor>> PostConductor(int techType, ConductorDTO conductorDTO)
        {
          if (_context.Conductor == null)
          {
              return Problem("Entity set 'APIConductorContext.Conductor'  is null.");
          }
            try
            {
                Conductor conductor = new();
                var address = await _addressesService.RetrieveAdressAPI(conductorDTO.Address);
                conductor.Document = conductorDTO.Document;
                conductor.Name = conductorDTO.Name;
                conductor.DateOfBirth = conductorDTO.DateOfBirth;
                conductor.Phone = conductorDTO.Phone;
                conductor.Email = conductorDTO.Email;
                conductor.Address = address;
                conductor.Address.CEP = conductorDTO.Address.CEP;
                conductor.Address.StreetType = conductorDTO.Address.StreetType;
                conductor.Address.Street = conductorDTO.Address.Complement;
                conductor.Address.Number = conductorDTO.Address.Number;
                conductor.CNH.DriverLicense = conductorDTO.CNHDTO.DriverLicense;

                int addressid, cnh, category;
                switch (techType)
                {
                    case 0:
                        _context.Conductor.Add(conductor);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        addressid = new AddressController().Insert(conductor.Address, 0);
                        cnh = new CNHController().Insert(conductor.CNH, 0);
                        category = new CategoryController().Insert(conductor.CNH.Category, 0);
                        conductor.Address.Id = addressid;
                        conductor.CNH.DriverLicense = cnh;
                        new ConductorController().Insert(conductor, 0);
                        break;
                    case 2:
                        addressid = new AddressController().Insert(conductor.Address, 1);
                        cnh = new CNHController().Insert(conductor.CNH, 1);
                        category = new CategoryController().Insert(conductor.CNH.Category, 1);
                        conductor.Address.Id = addressid;
                        conductor.CNH.DriverLicense = cnh;
                        new ConductorController().Insert(conductor, 1);
                        break;
                }
            }
            catch (DbUpdateException)
            {
                if (ConductorExists(conductorDTO.Document))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetConductor", new { id = conductorDTO.Document }, conductorDTO);
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
