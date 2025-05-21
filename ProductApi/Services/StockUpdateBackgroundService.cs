using ProductApi.Data;

namespace ProductApi.Services
{
    public class StockUpdateBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IStockUpdateQueue _queue;

        public StockUpdateBackgroundService(IServiceProvider serviceProvider, IStockUpdateQueue queue)
        {
            _serviceProvider = serviceProvider;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_queue.TryDequeue(out var message))
                {
                    using var scope = _serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    if (message != null)
                    {

                        var product = await dbContext.Products.FindAsync(message.ProductId);
                        if (product != null)
                        {
                            product.Stock = message.NewStock;
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }

                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
