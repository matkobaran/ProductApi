using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class Products2Controller : ControllerBase
    {
        private readonly AppDbContext _context;

        public Products2Controller(AppDbContext context)
        {
            _context = context;
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
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
