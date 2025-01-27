using ProductManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductManager.Data.Seeds;

public static class ProductSeed
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Laptop",
                Description = "High performance laptop",
                Price = 999.99m,
                ImgUri = "https://example.com/laptop.jpg",
                CrmId = Guid.Parse("21111111-1111-1111-1111-111111111111")
            },
            new Product
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Smartphone",
                Description = "Latest model smartphone",
                Price = 699.99m,
                ImgUri = "https://example.com/smartphone.jpg",
                CrmId = Guid.Parse("32222222-2222-2222-2222-222222222222")
            },
            new Product
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Headphones",
                Description = "Wireless noise-cancelling headphones",
                Price = 199.99m,
                ImgUri = "https://example.com/headphones.jpg",
                CrmId = Guid.Parse("43333333-3333-3333-3333-333333333333")
            }
        );
    }
}
