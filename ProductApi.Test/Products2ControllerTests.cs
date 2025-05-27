using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductApi.Controllers;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Seed;
using ProductApi.Services;

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
        public async Task GetAllProducts_CorrectInput_ShouldReturnProducts()
        {

            var context = GetDbContext();
            var mockQueue = new Mock<IStockUpdateQueue>();
            var controller = new Products2Controller(context, mockQueue.Object);
            var page = 2;
            var pageSize = 5;

            var result = await controller.GetAllProducts(page, pageSize);
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(actionResult.Value);

            Assert.Equal(pageSize, products.Count());
            Assert.All(products, p => Assert.InRange(p.ID, 6, 10));

        }

        [Fact]
        public async Task GetAllProducts_IncorrectInput_ShouldReturnProducts()
        {

            var context = GetDbContext();
            var mockQueue = new Mock<IStockUpdateQueue>();
            var controller = new Products2Controller(context, mockQueue.Object);

            var result = await controller.GetAllProducts(-1, 1);
            var actionResult = Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task UpdateProductStock_ExistingId_ShouldEnqueueStockUpdate()
        {
            var context = GetDbContext();
            var mockQueue = new Mock<IStockUpdateQueue>();
            var controller2 = new Products2Controller(context, mockQueue.Object);

            var productId = 2;
            var newStock = 99;

            var result = await controller2.UpdateStock(productId, newStock);

            var accepted = Assert.IsType<AcceptedResult>(result);
            mockQueue.Verify(q => q.Enqueue(It.Is<StockUpdateMessage>(
                m => m.ProductId == productId && m.NewStock == newStock
            )), Times.Once);
        }


        [Fact]
        public async Task UpdateProductStock_InvalidId_ShouldReturnNotFound()
        {
            var context = GetDbContext();
            var mockQueue = new Mock<IStockUpdateQueue>();
            var controller2 = new Products2Controller(context, mockQueue.Object);

            var invalidId = 999;

            var result = await controller2.UpdateStock(invalidId, 10);

            Assert.IsType<NotFoundResult>(result);
            mockQueue.Verify(q => q.Enqueue(It.IsAny<StockUpdateMessage>()), Times.Never);
        }

        [Fact]
        public async Task UpdateProductStock_NegativeStock_ShouldReturnNotFound()
        {
            var context = GetDbContext();
            var mockQueue = new Mock<IStockUpdateQueue>();
            var controller2 = new Products2Controller(context, mockQueue.Object);

            var invalidStock = -1;

            var result = await controller2.UpdateStock(1, invalidStock);

            Assert.IsType<BadRequestObjectResult>(result);
            mockQueue.Verify(q => q.Enqueue(It.IsAny<StockUpdateMessage>()), Times.Never);
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
