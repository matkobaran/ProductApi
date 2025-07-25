﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// List all products
        /// </summary>
        /// <returns>Return all products</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return single product</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid product ID");
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>New created product</returns>
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (product == null || product.Name == "" || product.ImageUrl == "")
            {
                return BadRequest("Invalid input");
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new { id = product.ID }, product);
        }

        /// <summary>
        /// Update product stock
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newStock"></param>
        [HttpPatch("{id}/updatestock")]
        public async Task<ActionResult> UpdateStock(int id, [FromBody] int newStock)
        {
            if (id < 0)
            {
                return BadRequest("Invalid product ID");
            }

            if (newStock < 0)
            {
                return BadRequest("Invalid stock number");
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Stock = newStock;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
