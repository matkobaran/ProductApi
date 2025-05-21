using System.Collections.Concurrent;
using ProductApi.Models;

namespace ProductApi.Services
{
    public class InMemoryStockUpdateQueue : IStockUpdateQueue
    {
        private readonly ConcurrentQueue<StockUpdateMessage> _queue = new();

        public void Enqueue(StockUpdateMessage message)
        {
            _queue.Enqueue(message);
        }

        public bool TryDequeue(out StockUpdateMessage? message)
        {
            return _queue.TryDequeue(out message);
        }
    }
}
