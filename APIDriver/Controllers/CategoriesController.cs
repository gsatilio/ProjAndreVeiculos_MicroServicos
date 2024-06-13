using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDriver.Data;
using Models;
using DataAPI.Data;
using APIDriver.Services;
using System.Runtime.ConstrainedExecution;

namespace APIDriver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataAPIContext _context;
        private readonly CategoriesService _service = new();

        public CategoriesController(DataAPIContext context, CategoriesService service)
        {
            _context = context;
            _service = service;
        }
        
        // GET: api/Categories/5
        [HttpGet("{techType}")]
        public async Task<ActionResult<List<Category>>> GetCategory(int techType)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            List<Category>? category = new List<Category>();
            switch (techType)
            {
                case 0:
                    category = await _context.Category.ToListAsync();
                    break;
                case 1:
                    category = await _service.GetAll(0);
                    break;
                case 2:
                    category = await _service.GetAll(1);
                    break;
                default:
                    return NotFound();
            }

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }
        
        // GET: api/Categories/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Category>> GetCategory(int id, int techType)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            Category? category = new Category();
            switch (techType)
            {
                case 0:
                    category = await _context.Category.FindAsync(id);
                    break;
                case 1:
                    category = await _service.Get(id, 0);
                    break;
                case 2:
                    category = await _service.Get(id, 1);
                    break;
                default:
                    return NotFound();
            }

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }
        
        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        
        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{techType}")]
        public async Task<ActionResult<Category>> PostCategory(int techType, Category category)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'APIDriverContext.Category'  is null.");
            }
            try
            {
                switch (techType)
                {
                    case 0:
                        _context.Category.Add(category);
                        await _context.SaveChangesAsync();
                        break;
                    case 1:
                        category.Id = await _service.Insert(category, 0);
                        break;
                    case 2:
                        category.Id = await _service.Insert(category, 1);
                        break;
                    default:
                        return NotFound();
                }
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(category.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id, techType }, category);
        }
        
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
