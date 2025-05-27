using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Controllers;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Seed;

namespace ProductApi.Test
{
    public class ProductsControllerTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Products.AddRange(SeedData.GetProducts());
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnProducts()
        {

            var context = GetDbContext();
            var controller = new ProductsController(context);

            var result = await controller.GetAllProducts();
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(actionResult.Value);

            Assert.Equal(13, products.Count());
        }

        [Fact]
        public async Task GetProductById_ExistingId_ShouldReturnProduct()
        {

            var context = GetDbContext();
            var controller = new ProductsController(context);

            var result = await controller.GetProductById(2);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsAssignableFrom<Product>(actionResult.Value);
            Assert.Equal("Keyboard", product.Name);
        }

        [Fact]
        public async Task GetProductById_NonExistingId_ShouldReturnNotFound()
        {
            var context = GetDbContext();
            var controller = new ProductsController(context);

            var result = await controller.GetProductById(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateProduct_ValidInput_ShouldReturnProduct()
        {
            var context = GetDbContext();
            var controller = new ProductsController(context);

            var initialResult = await controller.GetAllProducts();
            var initialActionResult = Assert.IsType<OkObjectResult>(initialResult.Result);
            var initialProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(initialActionResult.Value);
            var initialCount = initialProducts.Count();

            var newProduct = new Product
            {
                Name = "Test product #14",
                ImageUrl = "https://alza.cz"
            };

            var result = await controller.CreateProduct(newProduct);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var product = Assert.IsType<Product>(actionResult.Value);
            Assert.Equal("Test product #14", product.Name);
            Assert.Equal("https://alza.cz", product.ImageUrl);

            var endResult = await controller.GetAllProducts();
            var endActionResult = Assert.IsType<OkObjectResult>(endResult.Result);
            var endProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(endActionResult.Value);
            var endCount = endProducts.Count();

            Assert.Equal(initialCount + 1, endCount);
        }

        [Fact]
        public async Task CreateProduct_WithoutImage_ShouldFail()
        {

            var context = GetDbContext();
            var controller = new ProductsController(context);

            var initialResult = await controller.GetAllProducts();
            var initialActionResult = Assert.IsType<OkObjectResult>(initialResult.Result);
            var initialProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(initialActionResult.Value);
            var initialCount = initialProducts.Count();

            var newProduct = new Product
            {
                Name = "Test product #14",
            };

            var result = await controller.CreateProduct(newProduct);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);

            var endResult = await controller.GetAllProducts();
            var endActionResult = Assert.IsType<OkObjectResult>(endResult.Result);
            var endProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(endActionResult.Value);
            var endCount = initialProducts.Count();
            Assert.Equal(initialCount, endCount);
        }

        [Fact]
        public async Task UpdateProductStock_ExistingId_ShouldUpdateStock()
        {

            var context = GetDbContext();
            var controller = new ProductsController(context);

            var productId = 2;

            var initialResult = await controller.GetProductById(productId);
            var initialActionResult = Assert.IsType<OkObjectResult>(initialResult.Result);
            var initialProduct = Assert.IsAssignableFrom<Product>(initialActionResult.Value);
            var initialStock = initialProduct.Stock;

            await controller.UpdateStock(productId, initialStock + 5);

            var result = await controller.GetProductById(productId);
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsAssignableFrom<Product>(actionResult.Value);
            Assert.Equal(initialStock + 5, product.Stock);
        }

        [Fact]
        public async Task UpdateProductStock_NonExistingId_ShouldFail()
        {

            var context = GetDbContext();
            var controller = new ProductsController(context);

            var productId = 999;

            await controller.UpdateStock(productId, 5);
            var result = await controller.GetProductById(productId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateProductStock_NegativeStock_ShouldFail()
        {

            var context = GetDbContext();
            var controller = new ProductsController(context);

            var productId = 2;
            var newStock = -1;

            var result = await controller.UpdateStock(productId, newStock);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
