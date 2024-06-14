using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIInsurance.Data;
using Models;
using DataAPI.Data;
using APIInsurance.Services;
using Models.DTO;

namespace APIInsurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancesController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly InsurancesService _service;

        public InsurancesController(DataAPIContext context, InsurancesService insurancesService)
        {
            _context = context;
            _service = insurancesService;
        }

        // GET: api/Insurances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insurance>>> GetInsurance()
        {
          if (_context.Insurance == null)
          {
              return NotFound();
          }
            return await _context.Insurance.Include(x => x.Car).Include(x => x.Customer).Include(x => x.MainConductor).Include(x => x.Customer.Address).ToListAsync();
        }

        // GET: api/Insurances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Insurance>> GetInsurance(int id)
        {
          if (_context.Insurance == null)
          {
              return NotFound();
          }
            var insurance = await _context.Insurance.Include(x => x.Car).Include(x => x.Customer).Include(x => x.MainConductor).Include(x => x.Customer.Address).SingleOrDefaultAsync(x => x.Id == id);

            if (insurance == null)
            {
                return NotFound();
            }

            return insurance;
        }

        // PUT: api/Insurances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsurance(int id, Insurance insurance)
        {
            if (id != insurance.Id)
            {
                return BadRequest();
            }

            _context.Entry(insurance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceExists(id))
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

        // POST: api/Insurances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Insurance>> PostInsurance(InsuranceDTO dto)
        {
          if (_context.Insurance == null)
          {
              return Problem("Entity set 'APIInsuranceContext.Insurance'  is null.");
          }
            //_context.Insurance.Add(insurance);
            Insurance insurance = new Insurance(dto);

            var customer = _context.Customer.Where(x => x.Document == dto.CustomerDocument).FirstOrDefault();
            if (customer == null)
                return BadRequest("Cliente não encontrado");
            var car = _context.Car.Where(x => x.LicensePlate == dto.CarLicense).FirstOrDefault();
            if (car == null)
                return BadRequest("Carro não encontrado");
            var conductor = _context.Conductor.Where(x => x.Document == dto.MainConductorDocument).FirstOrDefault();
            if (conductor == null)
                return BadRequest("Motorista não encontrado");

            insurance.Car = car;
            insurance.Customer = customer;
            insurance.MainConductor = conductor;

            _service.Insert(insurance, 2);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsurance", new { id = insurance.Id }, insurance);
        }

        // DELETE: api/Insurances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurance(int id)
        {
            if (_context.Insurance == null)
            {
                return NotFound();
            }
            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }

            _context.Insurance.Remove(insurance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsuranceExists(int id)
        {
            return (_context.Insurance?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
