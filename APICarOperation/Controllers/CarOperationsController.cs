using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICarOperation.Data;
using Models;
using Controllers;
using DataAPI.Data;
using APICarOperation.Services;
using System.Runtime.ConstrainedExecution;
using Models.DTO;
using DataAPI.Service;

namespace APICarOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarOperationsController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly CarOperationsService _service = new();
        private readonly DataAPIServices _serviceAPI = new();

        public CarOperationsController(DataAPIContext context, CarOperationsService service, DataAPIServices dataAPIServices)
        {
            _context = context;
            _service = service;
            _serviceAPI = dataAPIServices;
        }

        // GET: api/CarOperations
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<CarOperation>>> GetCarOperation(int techType)
        {
            if (_context.CarOperation == null)
            {
                return NotFound();
            }
            List<CarOperation> carOp = new List<CarOperation>();
            switch (techType)
            {
                case 0:
                    carOp = await _context.CarOperation.Include(c => c.Car).Include(o => o.Operation).ToListAsync();
                    break;
                case 1:
                    carOp = await _service.GetAll(0);
                    break;
                case 2:
                    carOp = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return carOp;
        }

        // GET: api/CarOperations/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<CarOperation>> GetCarOperation(int id, int techType)
        {
            if (_context.CarOperation == null)
            {
                return NotFound();
            }
            CarOperation? carOp = new CarOperation();
            switch (techType)
            {
                case 0:
                    carOp = await _context.CarOperation.Include(c => c.Car).Include(o => o.Operation).SingleOrDefaultAsync(c => c.Id == id);
                    break;
                case 1:
                    carOp = await _service.Get(id, 0);
                    break;
                case 2:
                    carOp = await _service.Get(id, 1);
                    break;
                default:
                    return NotFound();
            }

            if (carOp == null)
            {
                return NotFound();
            }

            return carOp;
        }

        // PUT: api/CarOperations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarOperation(int id, CarOperation carOperation)
        {
            if (id != carOperation.Id)
            {
                return BadRequest();
            }

            _context.Entry(carOperation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarOperationExists(id))
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

        // POST: api/CarOperations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{techType}")]
        public async Task<ActionResult<CarOperation>> PostCarOperation(int techType, CarOperationDTO carOperationDTO)
        {
            if (_context.CarOperation == null)
            {
                return Problem("Entity set 'APICarOperationContext.CarOperation'  is null.");
            }

            //var car = _context.Car.Where(x => x.LicensePlate == carOperationDTO.LicensePlate).FirstOrDefault();
            var car = await _serviceAPI.GetCarAPI(carOperationDTO.LicensePlate);
            if (car == null)
                return BadRequest("Carro inexistente");
            var operation = await _serviceAPI.GetOperationAPI(carOperationDTO.IdOperation);
            if (operation == null)
                return BadRequest("Serviço inexistente");

            CarOperation carOperation = new(carOperationDTO) { Car = car, Operation = operation };

            try
            {
                switch (techType)
                {
                    case 0:
                        _context.CarOperation.Add(carOperation);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        carOperation.Id = await _service.Insert(carOperation, 0);
                        break;
                    case 2:
                        carOperation.Id = await _service.Insert(carOperation, 1);
                        break;
                    default:
                        return NotFound();
                }
            }
            catch (DbUpdateException)
            {
                if (CarOperationExists(carOperation.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCarOperation", new { id = carOperation.Id, techType }, carOperation);
        }

        // DELETE: api/CarOperations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarOperation(int id)
        {
            if (_context.CarOperation == null)
            {
                return NotFound();
            }
            var carOperation = await _context.CarOperation.FindAsync(id);
            if (carOperation == null)
            {
                return NotFound();
            }

            _context.CarOperation.Remove(carOperation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarOperationExists(int id)
        {
            return (_context.CarOperation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
