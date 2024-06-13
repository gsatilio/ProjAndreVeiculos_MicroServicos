using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Controllers;
using DataAPI.Data;
using APIEmployee.Services;
using APIAddress.Services;
using System.Net;

namespace APIEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly RolesService _service = new();

        public RolesController(DataAPIContext context, RolesService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Role
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole(int techType)
        {
            if (_context.Role == null)
            {
                return NotFound();
            }
            List<Role> addresses = new List<Role>();
            switch (techType)
            {
                case 0:
                    addresses = await _context.Role.ToListAsync();
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

        // GET: api/Role/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Role>> GetRole(int id, int techType)
        {
            if (_context.Role == null)
            {
                return NotFound();
            }

            Role? address = new Role();
            switch (techType)
            {
                case 0:
                    address = await _context.Role.FindAsync(id);
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

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{techType}")]
        public async Task<ActionResult<Role>> PostRole(int techType, Role role)
        {
          if (_context.Role == null)
          {
              return Problem("Entity set 'APIRoleContext.Role'  is null.");
          }
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Role.Add(role);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        role.Id = await _service.Insert(role, 0);
                        break;
                    case 2:
                        role.Id = await _service.Insert(role, 1);
                        break;
                }
            }
            catch (DbUpdateException)
            {
                if (RoleExists(role.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRole", new { id = role.Id, techType }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (_context.Role == null)
            {
                return NotFound();
            }
            var role = await _context.Role.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Role.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return (_context.Role?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
