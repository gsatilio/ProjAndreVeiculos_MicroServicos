using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIAddress.Data;
using Models;
using Models.DTO;
using APIAddress.Services;
using Services;
using Newtonsoft.Json;
using System.Security.Policy;
using Humanizer;
using Controllers;

namespace APIAddress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly APIAddressContext _context;

        public AddressesController(APIAddressContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            return await _context.Address.ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, AddressDTO addressDTO)
        {
            /*if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;
            */
            AddressesService addressesService = new AddressesService();
            var address = await addressesService.RetrieveAdressAPI(addressDTO);
            address.Id = id;
            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{tipoTec}")]
        public async Task<ActionResult<Address>> PostAddress(int tipoTec, AddressDTO addressDTO)
        {
            if (_context.Address == null)
            {
                return Problem("Entity set 'APIAddressContext.Address'  is null.");
            }
            Address address = new();
            AddressesService addressesService = new AddressesService();
            address = await addressesService.RetrieveAdressAPI(addressDTO);

            switch (tipoTec)
            {
                case 0:
                    _context.Address.Add(address);
                    await _context.SaveChangesAsync();
                    break;
                case 1:
                    new AddressController().Insert(address, 0);
                    break;
                case 2:
                    new AddressController().Insert(address, 1);
                    break;
            }

            return CreatedAtAction("GetAddress", new { id = address.Id }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Address?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
