using System;
using Microsoft.EntityFrameworkCore;
using ProductManager.Entities;
using ProductManager.Data.Seeds;

namespace ProductManager.Data;

public class ApplicationDbContext: DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSeeding((context, _) =>
                {
                    var products = ProductSeed.SeedData();

                    foreach (var product in products)
                    {
                        if (!context.Set<Product>().Any(p => p.Name == product.Name))
                        {
                            context.Set<Product>().Add(product);
                        }
                    }

                    context.SaveChanges();
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    var products = ProductSeed.SeedData();

                    foreach (var product in products)
                    {
                        if (!await context.Set<Product>().AnyAsync(p => p.Name == product.Name))
                        {
                            await context.Set<Product>().AddAsync(product);
                        }
                    }

                    await context.SaveChangesAsync();
                });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                assembly: typeof(ApplicationDbContext).Assembly
            );
        }

        public DbSet<Product> Products { get; set; }
}
