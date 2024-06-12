using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIRole.Data;
using Models;
using Controllers;
using DataAPI.Data;

namespace APIRole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DataAPIContext _context;

        public RolesController(DataAPIContext context)
        {
            _context = context;
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
                    addresses = await new RoleController().GetAll(0);
                    break;
                case 2:
                    addresses = await new RoleController().GetAll(1);
                    break;
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
                    address = await new RoleController().Get(id, 0);
                    break;
                case 2:
                    address = await new RoleController().Get(id, 1);
                    break;
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
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
          if (_context.Role == null)
          {
              return Problem("Entity set 'APIRoleContext.Role'  is null.");
          }
            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
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
