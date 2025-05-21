using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using ProductApi.Seed;

namespace ProductApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(SeedData.GetProducts());
        }

    }
}
