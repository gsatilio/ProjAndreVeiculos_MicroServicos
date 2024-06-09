﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPayment.Data;
using Models;
using Controllers;

namespace APIPayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly APIPaymentContext _context;

        public PaymentsController(APIPaymentContext context)
        {
            _context = context;
        }

        // GET: api/Payment
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayment(int techType)
        {
            if (_context.Payment == null)
            {
                return NotFound();
            }
            List<Payment> addresses = new List<Payment>();
            switch (techType)
            {
                case 0:
                    addresses = await _context.Payment.Include(p => p.CreditCard).Include(p => p.Boleto).Include(p => p.Pix.PixType).ToListAsync();
                    break;
                case 1:
                    addresses = await new PaymentController().GetAll(0);
                    break;
                case 2:
                    addresses = await new PaymentController().GetAll(1);
                    break;
            }
            return addresses;
        }

        // GET: api/Payment/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Payment>> GetPayment(int id, int techType)
        {
            if (_context.Payment == null)
            {
                return NotFound();
            }

            Payment? address = new Payment();
            switch (techType)
            {
                case 0:
                    address = await _context.Payment.Include(p => p.CreditCard).Include(p => p.Boleto).Include(p => p.Pix.PixType).SingleOrDefaultAsync(p => p.Id == id);
                    break;
                case 1:
                    address = await new PaymentController().Get(id, 0);
                    break;
                case 2:
                    address = await new PaymentController().Get(id, 1);
                    break;
            }

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
          if (_context.Payment == null)
          {
              return Problem("Entity set 'APIPaymentContext.Payment'  is null.");
          }
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new { id = payment.Id }, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (_context.Payment == null)
            {
                return NotFound();
            }
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return (_context.Payment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
