using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManager.Entities;

namespace ProductManager.Data.Configurations;

internal class DocumentConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(d => d.Name).IsRequired();
        builder.Property(d => d.ImgUri).IsRequired();
        builder.Property(d => d.Price).IsRequired().HasPrecision(18, 2);
        builder.Property(d => d.Description).HasMaxLength(2000);
        builder.Property(d => d.CrmId).IsRequired();
    }
}
