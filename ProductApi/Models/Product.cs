namespace ProductApi.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
    }
}
