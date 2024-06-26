﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIOperation.Data;
using Models;
using Controllers;
using DataAPI.Data;
using APIOperation.Services;
using System.Runtime.ConstrainedExecution;

namespace APIOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly OperationsService _service = new();

        public OperationsController(DataAPIContext context, OperationsService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Operation
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Operation>>> GetOperation(int techType)
        {
            if (_context.Operation == null)
            {
                return NotFound();
            }
            List<Operation> addresses = new List<Operation>();
            switch (techType)
            {
                case 0:
                    addresses = await _context.Operation.ToListAsync();
                    break;
                case 1:
                    addresses = await _service.GetAll(0);
                    break;
                case 2:
                    addresses = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return addresses;
        }

        // GET: api/Operation/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Operation>> GetOperation(int id, int techType)
        {
            if (_context.Operation == null)
            {
                return NotFound();
            }

            Operation? address = new Operation();
            switch (techType)
            {
                case 0:
                    address = await _context.Operation.FindAsync(id);
                    break;
                case 1:
                    address = await _service.Get(id, 0);
                    break;
                case 2:
                    address = await _service.Get(id, 1);
                    break;
                default:
                    return NotFound();
            }

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Operations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperation(int id, Operation operation)
        {
            if (id != operation.Id)
            {
                return BadRequest();
            }

            _context.Entry(operation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationExists(id))
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

        // POST: api/Operations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{techType}")]
        public async Task<ActionResult<Operation>> PostOperation(int techType, Operation operation)
        {
            if (_context.Operation == null)
            {
                return Problem("Entity set 'APIOperationContext.Operation'  is null.");
            }
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Operation.Add(operation);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        operation.Id = await _service.Insert(operation, 0);
                        break;
                    case 2:
                        operation.Id = await _service.Insert(operation, 1);
                        break;
                    default:
                        return NotFound();
                }
            }
            catch (DbUpdateException)
            {
                if (OperationExists(operation.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOperation", new { id = operation.Id, techType }, operation);
        }

        // DELETE: api/Operations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            if (_context.Operation == null)
            {
                return NotFound();
            }
            var operation = await _context.Operation.FindAsync(id);
            if (operation == null)
            {
                return NotFound();
            }

            _context.Operation.Remove(operation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperationExists(int id)
        {
            return (_context.Operation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
