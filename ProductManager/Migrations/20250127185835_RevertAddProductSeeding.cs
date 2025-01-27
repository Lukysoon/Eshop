using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManager.Migrations
{
    /// <inheritdoc />
    public partial class RevertAddProductSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CrmId", "Description", "ImgUri", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "High performance laptop", "https://example.com/laptop.jpg", "Laptop", 999.99m },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("32222222-2222-2222-2222-222222222222"), "Latest model smartphone", "https://example.com/smartphone.jpg", "Smartphone", 699.99m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Wireless noise-cancelling headphones", "https://example.com/headphones.jpg", "Headphones", 199.99m }
                });
        }
    }
}
