using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Controllers;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Seed;

namespace ProductApi.Test
{
    public class ProductUnitTest
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
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(result.Value);

            Assert.Equal(13, products.Count());
        }

        [Fact]
        public async Task GetSecondProduct_ShouldReturnSecondProduct()
        {

            var context = GetDbContext();
            var controller = new ProductsController(context);

            var result = await controller.GetProductById(2);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsAssignableFrom<Product>(actionResult.Value);
            Assert.Equal("Keyboard", product.Name);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnProduct()
        {

            var context = GetDbContext();
            var controller = new ProductsController(context);
            var newProduct = new Product
            {
                ID = 14,
                Name = "Test product #14",
                ImageUrl = "https://alza.cz"
            };

            var result = await controller.CreateProduct(newProduct);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsType<Product>(actionResult.Value);
            Assert.Equal("Test product #14", product.Name);
            Assert.Equal("https://alza.cz", product.ImageUrl);

            var productsResult = await controller.GetAllProducts();

            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(productsResult.Value);
            Assert.Equal(14, products.Count());
        }

        [Fact]
        public async Task UpdateThirdProductStock_ShouldReturnSuccess()
        {

            var context = GetDbContext();
            var controller = new ProductsController(context);

            await controller.UpdateStock(3, 10);
            var result = await controller.GetProductById(3);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsAssignableFrom<Product>(actionResult.Value);
            Assert.Equal(10, product.Stock);
        }
    }
}
