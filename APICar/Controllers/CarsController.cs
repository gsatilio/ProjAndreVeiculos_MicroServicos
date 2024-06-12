using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICar.Data;
using Models;
using Controllers;
using DataAPI.Data;
using APICar.Services;

namespace APICar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly CarsService _service = new();

        public CarsController(DataAPIContext context, CarsService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Cars
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar(int techType)
        {
            if (_context.Car == null)
            {
                return NotFound();
            }

            List<Car> cars = new List<Car>();
            switch (techType)
            {
                case 0:
                    cars = await _context.Car.ToListAsync();
                    break;
                case 1:
                    cars = await _service.GetAll(0);
                    break;
                case 2:
                    cars = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return cars;
        }

        // GET: api/Cars/5
        [HttpGet("{licensePlate},{techType}")]
        public async Task<ActionResult<Car>> GetCar(string licensePlate, int techType)
        {
            if (_context.Car == null)
            {
                return NotFound();
            }
            Car? car = new Car();
            switch (techType)
            {
                case 0:
                    car = await _context.Car.FindAsync(licensePlate);
                    break;
                case 1:
                    car = await _service.Get(licensePlate, 0);
                    break;
                case 2:
                    car = await _service.Get(licensePlate, 1);
                    break;
                default:
                    return NotFound();
            }

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(string id, Car car)
        {
            if (id != car.LicensePlate)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'APICarContext.Car'  is null.");
            }
            _context.Car.Add(car);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarExists(car.LicensePlate))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCar", new { id = car.LicensePlate }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(string id)
        {
            if (_context.Car == null)
            {
                return NotFound();
            }
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(string id)
        {
            return (_context.Car?.Any(e => e.LicensePlate == id)).GetValueOrDefault();
        }
    }
}
