using System;
using Microsoft.EntityFrameworkCore;
using ProductManager.Entities;

namespace ProductManager.Data;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
