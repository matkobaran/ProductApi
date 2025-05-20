using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Test
{
    public class TestData
    {
        public static List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product { ID = 1, Name = "Laptop", Price = 1200, Description = "Gaming laptop", Stock = 10 },
                new Product { ID = 2, Name = "Keyboard", ImageUrl = "www.alza.cz", Price = 80 },
                new Product { ID = 3, Name = "Mouse", ImageUrl = "www.alza.cz", Price = 50, Description = "Wireless mouse", Stock = 15 },
                new Product { ID = 4, Name = "Smartphone", ImageUrl = "www.alza.cz", Price = 850, Description = "Wireless mouse", Stock = 4 },
                new Product { ID = 5, Name = "Headphones", Price = 90, Description = "Nice headphones", Stock = 99 },
                new Product { ID = 6, Name = "Lego", ImageUrl = "https://image.alza.cz/products/LO42207/LO42207.jpg?width=1400&height=1400", Price = 199.9M, Description = "Lego Technic 42207 Auto Ferrari SF-24 F1", Stock = 2 }
            };
        }
    }
}
