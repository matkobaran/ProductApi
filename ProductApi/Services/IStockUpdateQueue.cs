using ProductApi.Models;

namespace ProductApi.Services
{
    public interface IStockUpdateQueue
    {
        void Enqueue(StockUpdateMessage message);
        bool TryDequeue(out StockUpdateMessage? message);
    }
}
