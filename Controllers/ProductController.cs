using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _dbContext;

        public ProductController(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [EnableCors]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            if(_dbContext.Products == null)
            {
                return NotFound();
            }
            return await _dbContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        [EnableCors]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            if(_dbContext.Products == null)
            {
                return NotFound();
            }
            var products = await _dbContext.Products.FindAsync(id);

            if(products == null)
            {
                return NotFound();
            }

            return products;
        }

        [HttpPost]
        [EnableCors]
        public async Task<ActionResult<Products>> PostProducts(Products items)
        {
            _dbContext.Products.Add(items);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducts), new { id = items.Id }, items);
        }

        [HttpPut("{id}")]
        [EnableCors]
        public async Task<IActionResult> PutProduct(int id, Products items)
        {
            if(id != items.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(items).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!ProductsExist(id))
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

        [HttpDelete("{id}")]
        [EnableCors]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_dbContext.Products == null)
            {
                return NotFound();
            }

            var item = await _dbContext.Products.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(item);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsExist(int id)
        {
            return (_dbContext.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
