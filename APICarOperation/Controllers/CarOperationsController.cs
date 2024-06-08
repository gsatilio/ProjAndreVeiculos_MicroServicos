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

namespace APICarOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarOperationsController : ControllerBase
    {
        private readonly APICarOperationContext _context;

        public CarOperationsController(APICarOperationContext context)
        {
            _context = context;
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
                    carOp = await new CarOperationController().GetAll(0);
                    break;
                case 2:
                    carOp = await new CarOperationController().GetAll(1);
                    break;
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
                    carOp = await _context.CarOperation.Include(c => c.Car).SingleOrDefaultAsync(c => c.Id == id);
                    break;
                case 1:
                    carOp = await new CarOperationController().Get(id, 0);
                    break;
                case 2:
                    carOp = await new CarOperationController().Get(id, 1);
                    break;
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
        [HttpPost]
        public async Task<ActionResult<CarOperation>> PostCarOperation(CarOperation carOperation)
        {
          if (_context.CarOperation == null)
          {
              return Problem("Entity set 'APICarOperationContext.CarOperation'  is null.");
          }
            _context.CarOperation.Add(carOperation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarOperation", new { id = carOperation.Id }, carOperation);
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
