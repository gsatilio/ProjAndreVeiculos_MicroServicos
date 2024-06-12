using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIConductor.Data;
using Models;
using Controllers;

namespace APIConductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly APIConductorContext _context;

        public CategoriesController(APIConductorContext context)
        {
            _context = context;
        }

        // GET: api/Categorys
        [HttpGet("{techType}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory(int techType)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            List<Category> addresses = new List<Category>();
            switch (techType)
            {
                case 0:
                    addresses = await _context.Category.ToListAsync();
                    break;
                case 1:
                    addresses = await new CategoryController().GetAll(0);
                    break;
                case 2:
                    addresses = await new CategoryController().GetAll(1);
                    break;
            }
            return addresses;
        }

        // GET: api/Categorys/5
        [HttpGet("{id},{techType}")]
        public async Task<ActionResult<Category>> GetCategory(int id, int techType)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }

            Category? address = new Category();
            switch (techType)
            {
                case 0:
                    address = await _context.Category.FindAsync(id);
                    break;
                case 1:
                    address = await new CategoryController().Get(id, 0);
                    break;
                case 2:
                    address = await new CategoryController().Get(id, 1);
                    break;
            }

            if (address == null)
            {
                return NotFound();
            }

            return address;
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
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
          if (_context.Category == null)
          {
              return Problem("Entity set 'APIConductorContext.Category'  is null.");
          }
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id, techType = 0 }, category);
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
