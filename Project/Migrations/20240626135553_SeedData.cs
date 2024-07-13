using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "IdDiscount", "DateFrom", "DateTo", "ForSubscription", "ForUpfront", "Name", "Percentage" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Discount A", 10 },
                    { 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, "Discount B", 20 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "IdProduct", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Category 1", "Description for Product A", "Product A", 100m },
                    { 2, "Category 2", "Description for Product B", "Product B", 200m },
                    { 3, "Category 3", "Description for Product C", "Product C", 300m }
                });

            migrationBuilder.InsertData(
                table: "ProductVersions",
                columns: new[] { "IdProductVersion", "IdProduct", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Version 1.0" },
                    { 2, 1, "Version 1.1" },
                    { 3, 2, "Version 2.0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVersions",
                keyColumn: "IdProductVersion",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductVersions",
                keyColumn: "IdProductVersion",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVersions",
                keyColumn: "IdProductVersion",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "IdProduct",
                keyValue: 2);
        }
    }
}
