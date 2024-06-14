using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIBank.Data;
using Models;
using DataAPI.Data;
using Models.DTO;
using APIBank.Services;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;

namespace APIBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly BanksService _service;
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "bank";

        public BanksController(DataAPIContext context, BanksService banksService, ConnectionFactory factory)
        {
            _context = context;
            _service = banksService;
            _factory = factory;
        }

        // GET: api/Banks
        [HttpGet]
        public ActionResult<List<Bank>> GetBank()
        {
            if (_context.Bank == null)
            {
                return NotFound();
            }
            return _service.GetAllMongo();
            /*
             new List<Bank>();
            switch (techType)
            {
                case 0:
                    banks = await _context.Bank.ToListAsync();
                    break;
                case 1:
                    banks = await _service.GetAll(0);
                    break;
                case 2:
                    banks = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }*/
        }

        // GET: api/Banks/5
        [HttpGet("{cnpj:length(14)}")]
        public async Task<ActionResult<Bank>> GetBank(string cnpj)
        {
            if (_context.Bank == null)
            {
                return NotFound();
            }

            return _service.GetMongoById(cnpj);
            /*
            Bank? bank = new Bank();
            switch (techType)
            {
                case 0:
                    bank = await _context.Bank.FindAsync(cnpj);
                    break;
                case 1:
                    bank = await _service.Get(cnpj, 0);
                    break;
                case 2:
                    bank = await _service.Get(cnpj, 1);
                    break;
                default:
                    return NotFound();
            }

            if (bank == null)
            {
                return NotFound();
            }

            return bank;
            */
        }


        // PUT: api/Banks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBank(string id, Bank bank)
        {
            if (id != bank.CNPJ)
            {
                return BadRequest();
            }

            _context.Entry(bank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(id))
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

        // POST: api/Bankes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostMQMessage([FromBody] Bank bank)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var stringfieldMessage = JsonConvert.SerializeObject(bank);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesMessage
                        );
                }
            }
            return Accepted();
        }
        /*
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            if (_context.Bank == null)
            {
                return Problem("Entity set 'APIBankContext.Bank'  is null.");
            }
            return _service.InsertMongo(bank);
            
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Bank.Add(bank);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        await _service.Insert(bank, 0);
                        break;
                    case 2:
                        await _service.Insert(bank, 1);
                        break;
                    default:
                        return NotFound();
                }
                _service.InsertMongo(bank);
            }
            catch (DbUpdateException)
            {
                if (BankExists(bank.CNPJ))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetBank", new { cnpj = bank.CNPJ, techtype = techType }, bank);
        }*/

        // DELETE: api/Banks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(string id)
        {
            if (_context.Bank == null)
            {
                return NotFound();
            }
            var bank = await _context.Bank.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }

            _context.Bank.Remove(bank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankExists(string id)
        {
            return (_context.Bank?.Any(e => e.CNPJ == id)).GetValueOrDefault();
        }
    }
}
