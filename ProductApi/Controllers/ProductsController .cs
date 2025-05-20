using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return all available products
        /// </summary>
        /// <returns>Return product</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Return single product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return product</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return product;
        }

        /// <summary>
        /// Create new product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>New created product</returns>
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        /// <summary>
        /// Update stock of the product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newStock"></param>
        [HttpPatch("{id}/stock")]
        public async Task<ActionResult> UpdateStock(int id, [FromBody] int newStock)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Stock = newStock;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
