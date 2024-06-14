using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DataAPI.Data;
using APIDependent.Services;
using Models.DTO;
using Services;
using APIAddress.Services;
using DataAPI.Service;

namespace APIDependent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependentsController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly DataAPIServices _apiService;
        private readonly DependentsService _service;

        public DependentsController(DataAPIContext context, DependentsService dependentsService, DataAPIServices dataAPIServices)
        {
            _context = context;
            _service = dependentsService;
            _apiService = dataAPIServices; 
        }

        // GET: api/Dependents
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Dependent>>> GetDependent(int techType)
        {
            if (_context.Dependent == null)
            {
                return NotFound();
            }
            List<Dependent> dependent = new List<Dependent>();
            switch (techType)
            {
                case 0:
                    dependent = await _context.Dependent.Include(a => a.Address).Include(b => b.Customer).ToListAsync();
                    break;
                case 1:
                    dependent = await _service.GetAll(0);
                    break;
                case 2:
                    dependent = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return dependent;
        }

        // GET: api/Dependents/5
        [HttpGet("{document},{techType}")]
        public async Task<ActionResult<Dependent>> GetDependent(string document, int techType)
        {
            if (_context.Dependent == null)
            {
                return NotFound();
            }
            Dependent? dependent = new Dependent();
            switch (techType)
            {
                case 0:
                    dependent = await _context.Dependent.Include(a => a.Address).Include(b => b.Customer).SingleOrDefaultAsync(c => c.Document == document);
                    break;
                case 1:
                    dependent = await _service.Get(document, 0);
                    break;
                case 2:
                    dependent = await _service.Get(document, 1);
                    break;
                default:
                    return NotFound();
            }

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
        public async Task<ActionResult<Dependent>> PostDependent(DependentDTO dependentDTO)
        {
            int techType = 1; // ADO
            if (_context.Dependent == null)
            {
                return Problem("Entity set 'APIDependentContext.Dependent'  is null.");
            }

            var customer = _context.Customer.Where(x => x.Document == dependentDTO.CustomerDocument).FirstOrDefault();
            if (customer == null)
                return BadRequest("Cliente titular não existe");

            var dependent = new Dependent(dependentDTO);
            var address = await _apiService.PostAddressAPI(dependentDTO.Address); // Chama API Address
            if (address == null)
                return BadRequest("Endereço não encontrado");

            dependent.Address = address;
            dependent.Customer = customer;
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Address.Add(address);
                        _context.Dependent.Add(dependent);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        _service.Insert(dependent, 0);
                        break;
                    case 2:
                        _service.Insert(dependent, 1);
                        break;
                }
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

            return CreatedAtAction("GetDependent", new { id = dependent.Document, techType }, dependent);
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
