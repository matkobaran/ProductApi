using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class Products2Controller : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IStockUpdateQueue _queue;

        public Products2Controller(AppDbContext context, IStockUpdateQueue queue)
        {
            _context = context;
            _queue = queue;
        }

        /// <summary>
        /// List all products with paginator
        /// </summary>
        /// <param name="pageNumber">Page number (default 1).</param>
        /// <param name="pageSize">Page size (default 10).</param>
        /// <returns>Paged list of products.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Products
                .OrderBy(p => p.ID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Update product stock
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newStock"></param>
        [HttpPost("{id}/updatestock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] int newStock)
        {
            var exists = await _context.Products.AnyAsync(p => p.ID == id);
            if (!exists)
                return NotFound();

            _queue.Enqueue(new StockUpdateMessage
            {
                ProductId = id,
                NewStock = newStock
            });

            return Accepted();
        }
    }
}
