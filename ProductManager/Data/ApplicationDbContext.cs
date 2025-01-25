using System;
using Microsoft.EntityFrameworkCore;
using ProductManager.Entities;

namespace ProductManager.Data;

public class ApplicationDbContext: DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                assembly: typeof(ApplicationDbContext).Assembly
            );
        }

        public DbSet<Product> Products { get; set; }
}
