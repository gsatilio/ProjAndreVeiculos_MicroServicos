using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIEmployee.Data;
using Models;
using Controllers;
using Models.DTO;
using APIAddress.Services;
using DataAPI.Data;
using APIEmployee.Services;

namespace APIEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly AddressesService _addressesService;
        private readonly EmployeesService _service = new();

        public EmployeesController(DataAPIContext context, EmployeesService service, AddressesService addressesService)
        {
            _context = context;
            _service = service;
            _addressesService = addressesService;
        }
        /*public EmployeesController(DataAPIContext context, AddressesService addressesService)
        {
            _context = context;
            _addressesService = addressesService;
        }*/

        // GET: api/Employees
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(int techType)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            List<Employee> customer = new List<Employee>();
            switch (techType)
            {
                case 0:
                    customer = await _context.Employee.Include(a => a.Address).Include(b => b.Role).ToListAsync();
                    break;
                case 1:
                    customer = await _service.GetAll(0);
                    break;
                case 2:
                    customer = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return customer;
        }

        // GET: api/Employees/5
        [HttpGet("{document},{techType}")]
        public async Task<ActionResult<Employee>> GetEmployee(string document, int techType)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            Employee? customer = new Employee();
            switch (techType)
            {
                case 0:
                    customer = await _context.Employee.Include(a => a.Address).Include(b => b.Role).SingleOrDefaultAsync(c => c.Document == document);
                    break;
                case 1:
                    customer = await _service.Get(document, 0);
                    break;
                case 2:
                    customer = await _service.Get(document, 1);
                    break;
                default:
                    return NotFound();
            }

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(string id, Employee employee)
        {
            if (id != employee.Document)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{techType}")]
        public async Task<ActionResult<Employee>> PostEmployee(int techType, EmployeeDTO employeeDTO)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'APIEmployeeContext.Employee'  is null.");
            }
            var employee = new Employee(employeeDTO);
            var address = await _addressesService.RetrieveAdressAPI(employeeDTO.Address);

            if (address == null)
                return BadRequest("Endereço não existente");

            var role = _context.Role.Where(x => x.Id == employeeDTO.RoleId).FirstOrDefault();
            if (role == null)
                return BadRequest("Cargo não existente");

            employee.Address = address;
            employee.Role = role;
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Employee.Add(employee);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        employee.Address.Id = await _addressesService.Insert(address, 0);
                        new EmployeeController().Insert(employee, 0);
                        break;
                    case 2:
                        employee.Address.Id = await _addressesService.Insert(address, 1);
                        new EmployeeController().Insert(employee, 1);
                        break;
                }
                _addressesService.InsertMongo(address);
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.Document))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            
            return CreatedAtAction("GetEmployee", new { document = employee.Document, techType = 0 }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(string id)
        {
            return (_context.Employee?.Any(e => e.Document == id)).GetValueOrDefault();
        }
    }
}
