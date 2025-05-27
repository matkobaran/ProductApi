using ProductApi.Models;

namespace ProductApi.Seed;

public static class SeedData
{
     public static List<Product> GetProducts()
        {
            return
            [
                new() { ID = 1, Name = "Laptop", ImageUrl = "www.alza.cz", Price = 1200, Description = "Gaming laptop", Stock = 10 },
                new() { ID = 2, Name = "Keyboard", ImageUrl = "www.alza.cz", Price = 80 },
                new() { ID = 3, Name = "Mouse", ImageUrl = "www.alza.cz", Price = 50, Description = "Wireless mouse", Stock = 15 },
                new() { ID = 4, Name = "Smartphone", ImageUrl = "www.alza.cz", Price = 850, Description = "Wireless mouse", Stock = 4 },
                new() { ID = 5, Name = "Headphones", ImageUrl = "www.alza.cz", Price = 90, Description = "Nice headphones", Stock = 99 },
                new() { ID = 6, Name = "Lego", ImageUrl = "https://image.alza.cz/products/LO42207/LO42207.jpg?width=1400&height=1400", Price = 199.9M, Description = "Lego Technic 42207 Auto Ferrari SF-24 F1", Stock = 2 },
                new () { ID = 7, Name = "Product #7", ImageUrl = "www.alza.cz", Price = 87 },
                new () { ID = 8, Name = "Product #8", ImageUrl = "www.alza.cz", Price = 88 },
                new () { ID = 9, Name = "Product #9", ImageUrl = "www.alza.cz", Price = 89 },
                new () { ID = 10, Name = "Product #10", ImageUrl = "www.alza.cz", Price = 90 },
                new () { ID = 11, Name = "Product #11", ImageUrl = "www.alza.cz", Price = 91 },
                new () { ID = 12, Name = "Product #12", ImageUrl = "www.alza.cz", Price = 92 },
                new () { ID = 13, Name = "Product #13", ImageUrl = "www.alza.cz", Price = 93 }
            ];
        }
}
