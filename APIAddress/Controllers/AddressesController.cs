﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIAddress.Data;
using Models;
using Models.DTO;
using APIAddress.Services;
using DataAPI.Data;

namespace APIAddress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly AddressesService _service;
        public AddressesController(DataAPIContext context, AddressesService addressesService)
        {
            _context = context;
            _service = addressesService;
        }

        // GET: api/Addresses
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress(int techType)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            List<Address> addresses = new List<Address>();
            switch (techType)
            {
                case 0:
                    addresses = await _context.Address.ToListAsync();
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

        // GET: api/Addresses/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Address>> GetAddress(int id, int techType)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }

            Address? address = new Address();
            switch (techType)
            {
                case 0:
                    address = await _context.Address.FindAsync(id);
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
            Address address = new();
            address.CEP = addressDTO.CEP;
            address.Complement = addressDTO.Complement;
            address.Number = addressDTO.Number;
            address.StreetType = addressDTO.StreetType;

            address = await _service.RetrieveAdressAPI(addressDTO);
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
        [HttpPost("{techType}")]
        public async Task<ActionResult<Address>> PostAddress(int techType, AddressDTO addressDTO)
        {
            if (_context.Address == null)
            {
                return Problem("Entity set 'APIAddressContext.Address'  is null.");
            }
            var address = await _service.RetrieveAdressAPI(addressDTO);

            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Address.Add(address);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        address.Id = await _service.Insert(address, 0);
                        break;
                    case 2:
                        address.Id = await _service.Insert(address, 1);
                        break;
                    default:
                        return NotFound();
                }
                _service.InsertMongo(address);
            }
            catch (DbUpdateException)
            {
                if (AddressExists(address.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetAddress", new { id = address.Id, techtype = techType }, address);
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
