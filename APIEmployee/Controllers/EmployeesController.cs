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
using DataAPI.Service;
using MongoDB.Driver.Linq;

namespace APIEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly DataAPIServices _apiService;
        private readonly EmployeesService _service = new();

        public EmployeesController(DataAPIContext context, EmployeesService service, DataAPIServices dataAPIServices)
        {
            _context = context;
            _service = service;
            _apiService = dataAPIServices;
        }

        // GET: api/Employees
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(int techType)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            List<Employee> employee = new List<Employee>();
            switch (techType)
            {
                case 0:
                    employee = await _context.Employee.Include(a => a.Address).Include(b => b.Role).ToListAsync();
                    /*List<Address> addresses = new List<Address>();
                    addresses = await _apiService.GetAllAddressAPI(); // Chama API Address
                    foreach (var emp in employee)
                    {
                        foreach (var add in addresses.Where(x => x.Id == emp.Address.Id))
                        {
                            emp.Address = add;
                        }
                    }
                    employee = await _context.Employee.Include(b => b.Role).ToListAsync();*/
                    break;
                case 1:
                    employee = await _service.GetAll(0);
                    break;
                case 2:
                    employee = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }
            return employee;
        }

        // GET: api/Employees/5
        [HttpGet("{document},{techType}")]
        public async Task<ActionResult<Employee>> GetEmployee(string document, int techType)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            Employee? employee = new Employee();
            switch (techType)
            {
                case 0:
                    employee = await _context.Employee.Include(a => a.Address).Include(b => b.Role).SingleOrDefaultAsync(c => c.Document == document);
                    /*Address address = new Address();
                    employee.Address = await _apiService.GetAddressAPI(employee.Address.Id); // Chama API Address
                    employee = await _context.Employee.Include(b => b.Role).SingleOrDefaultAsync(c => c.Document == document);*/
                    break;
                case 1:
                    employee = await _service.Get(document, 0);
                    break;
                case 2:
                    employee = await _service.Get(document, 1);
                    break;
                default:
                    return NotFound();
            }

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
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
            var address = await _apiService.PostAddressAPI(employeeDTO.Address); // Chama API Address

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
                        _context.Address.Attach(address);
                        _context.Role.Attach(role);
                        _context.Employee.Add(employee);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        _service.Insert(employee, 0);
                        break;
                    case 2:
                        _service.Insert(employee, 1);
                        break;
                }
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
