using ProductManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductManager.Data.Seeds;

public static class ProductSeed
{
    public static Product[] SeedData()
    {
        return new[]
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    ImgUri = "http://test.com/product1.jpg",
                    Price = 19.99m,
                    Description = "This is the first product.",
                    CrmId = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    ImgUri = "http://test.com/product2.jpg",
                    Price = 24.99m,
                    Description = "This is the second product.",
                    CrmId = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    ImgUri = "http://test.com/product3.jpg",
                    Price = 14.99m,
                    Description = "This is the third product.",
                    CrmId = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 4",
                    ImgUri = "http://test.com/product4.jpg",
                    Price = 29.99m,
                    Description = "This is the fourth product.",
                    CrmId = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 5",
                    ImgUri = "http://test.com/product5.jpg",
                    Price = 9.99m,
                    Description = "This is the fifth product.",
                    CrmId = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 6",
                    ImgUri = "http://test.com/product6.jpg",
                    Price = 39.99m,
                    Description = "This is the sixth product.",
                    CrmId = Guid.NewGuid()
                }
            };
    }
}
