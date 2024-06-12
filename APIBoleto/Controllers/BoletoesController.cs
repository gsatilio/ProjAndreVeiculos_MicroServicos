using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIBoleto.Data;
using Models;
using Controllers;
using DataAPI.Data;

namespace APIBoleto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletoesController : ControllerBase
    {
        private readonly DataAPIContext _context;

        public BoletoesController(DataAPIContext context)
        {
            _context = context;
        }

        // GET: api/Boleto
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Boleto>>> GetBoleto(int techType)
        {
            if (_context.Boleto == null)
            {
                return NotFound();
            }
            List<Boleto> addresses = new List<Boleto>();
            switch (techType)
            {
                case 0:
                    addresses = await _context.Boleto.ToListAsync();
                    break;
                case 1:
                    addresses = await new BoletoController().GetAll(0);
                    break;
                case 2:
                    addresses = await new BoletoController().GetAll(1);
                    break;
            }
            return addresses;
        }

        // GET: api/Boleto/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Boleto>> GetBoleto(int id, int techType)
        {
            if (_context.Boleto == null)
            {
                return NotFound();
            }

            Boleto? address = new Boleto();
            switch (techType)
            {
                case 0:
                    address = await _context.Boleto.FindAsync(id);
                    break;
                case 1:
                    address = await new BoletoController().Get(id, 0);
                    break;
                case 2:
                    address = await new BoletoController().Get(id, 1);
                    break;
            }

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Boletoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoleto(int id, Boleto boleto)
        {
            if (id != boleto.Id)
            {
                return BadRequest();
            }

            _context.Entry(boleto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoletoExists(id))
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

        // POST: api/Boletoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Boleto>> PostBoleto(Boleto boleto)
        {
          if (_context.Boleto == null)
          {
              return Problem("Entity set 'APIBoletoContext.Boleto'  is null.");
          }
            _context.Boleto.Add(boleto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoleto", new { id = boleto.Id }, boleto);
        }

        // DELETE: api/Boletoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoleto(int id)
        {
            if (_context.Boleto == null)
            {
                return NotFound();
            }
            var boleto = await _context.Boleto.FindAsync(id);
            if (boleto == null)
            {
                return NotFound();
            }

            _context.Boleto.Remove(boleto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoletoExists(int id)
        {
            return (_context.Boleto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
