using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICreditCard.Data;
using Models;
using Controllers;
using DataAPI.Data;

namespace APICreditCard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private readonly DataAPIContext _context;

        public CreditCardsController(DataAPIContext context)
        {
            _context = context;
        }

        // GET: api/CreditCard
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<CreditCard>>> GetCreditCard(int techType)
        {
            if (_context.CreditCard == null)
            {
                return NotFound();
            }
            List<CreditCard> addresses = new List<CreditCard>();
            switch (techType)
            {
                case 0:
                    addresses = await _context.CreditCard.ToListAsync();
                    break;
                case 1:
                    addresses = await new CreditCardController().GetAll(0);
                    break;
                case 2:
                    addresses = await new CreditCardController().GetAll(1);
                    break;
            }
            return addresses;
        }

        // GET: api/CreditCard/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<CreditCard>> GetCreditCard(int id, int techType)
        {
            if (_context.CreditCard == null)
            {
                return NotFound();
            }

            CreditCard? address = new CreditCard();
            switch (techType)
            {
                case 0:
                    address = await _context.CreditCard.FindAsync(id);
                    break;
                case 1:
                    address = await new CreditCardController().Get(id, 0);
                    break;
                case 2:
                    address = await new CreditCardController().Get(id, 1);
                    break;
            }

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/CreditCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditCard(int id, CreditCard creditCard)
        {
            if (id != creditCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(creditCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCardExists(id))
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

        // POST: api/CreditCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreditCard>> PostCreditCard(CreditCard creditCard)
        {
          if (_context.CreditCard == null)
          {
              return Problem("Entity set 'APICreditCardContext.CreditCard'  is null.");
          }
            _context.CreditCard.Add(creditCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreditCard", new { id = creditCard.Id }, creditCard);
        }

        // DELETE: api/CreditCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditCard(int id)
        {
            if (_context.CreditCard == null)
            {
                return NotFound();
            }
            var creditCard = await _context.CreditCard.FindAsync(id);
            if (creditCard == null)
            {
                return NotFound();
            }

            _context.CreditCard.Remove(creditCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreditCardExists(int id)
        {
            return (_context.CreditCard?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
