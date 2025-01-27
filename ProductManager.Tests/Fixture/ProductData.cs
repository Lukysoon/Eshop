using System;
using ProductManager.Entities;
using ProductManager.Models.Dto;

namespace ProductManager.Tests.Data;

public static class ProductData
{
    public static List<Product> GetProducts()
    {
                // Setup test data
        return new List<Product>
        {
            new Product 
            { 
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Test Product 1",
                ImgUri = "test1.jpg",
                Price = 10.99m,
                Description = "Test Description 1",
                CrmId = Guid.NewGuid()
            },
            new Product 
            { 
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Test Product 2",
                ImgUri = "test2.jpg",
                Price = 20.99m,
                Description = "Test Description 2",
                CrmId = Guid.NewGuid()
            },
            new Product 
            { 
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Test Product 3",
                ImgUri = "test3.jpg",
                Price = 30.99m,
                Description = "Test Description 3",
                CrmId = Guid.NewGuid()
            },
            new Product 
            { 
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Test Product 4",
                ImgUri = "test4.jpg",
                Price = 40.99m,
                Description = "Test Description 4",
                CrmId = Guid.NewGuid()
            }
        };
    }

    public static List<ProductDto> GetProductDtos()
    {
        var products = GetProducts();

        return new List<ProductDto>
        {
            new ProductDto 
            { 
                Id = products[0].Id,
                Name = products[0].Name,
                ImgUri = products[0].ImgUri,
                Price = products[0].Price,
                Description = products[0].Description
            },
            new ProductDto 
            { 
                Id = products[1].Id,
                Name = products[1].Name,
                ImgUri = products[1].ImgUri,
                Price = products[1].Price,
                Description = products[1].Description
            },
            new ProductDto 
            { 
                Id = products[2].Id,
                Name = products[2].Name,
                ImgUri = products[2].ImgUri,
                Price = products[2].Price,
                Description = products[2].Description
            },
            new ProductDto 
            { 
                Id = products[3].Id,
                Name = products[3].Name,
                ImgUri = products[3].ImgUri,
                Price = products[3].Price,
                Description = products[3].Description
            }
        };
    }
}
