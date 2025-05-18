using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace walks.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDiffcicultiesAndRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("b0f1a2c4-5d3e-4b8c-9f7d-6a2e5f3b8c1e"), "Medium" },
                    { new Guid("b0f1a2c4-5d3e-4b8c-9f7d-6a2e5f3b8c2d"), "Easy" },
                    { new Guid("cdf1a2c4-5d3e-4b8c-9f7d-6a2e5f3b8c1e"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4a8c-9b7d-6a2e5f3b8c2d"), "NI", "North Island", "https://example.com/north-island.jpg" },
                    { new Guid("b1c2d3e4-f5a6-4b8c-8d7e-7b3c6d2e4f1a"), "SI", "South Island", "https://example.com/south-island.jpg" },
                    { new Guid("c1d2e3f4-a5b6-4c8d-9e7f-1a2b3c4d5e6f"), "STI", "Stewart Island", "https://example.com/stewart-island.jpg" },
                    { new Guid("d1e2f3a4-b5c6-4d7e-8f9a-2b3c4d5e6f7a"), "CI", "Chatham Islands", "https://example.com/chatham-islands.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b0f1a2c4-5d3e-4b8c-9f7d-6a2e5f3b8c1e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b0f1a2c4-5d3e-4b8c-9f7d-6a2e5f3b8c2d"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("cdf1a2c4-5d3e-4b8c-9f7d-6a2e5f3b8c1e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a8c-9b7d-6a2e5f3b8c2d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b1c2d3e4-f5a6-4b8c-8d7e-7b3c6d2e4f1a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-a5b6-4c8d-9e7f-1a2b3c4d5e6f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d1e2f3a4-b5c6-4d7e-8f9a-2b3c4d5e6f7a"));
        }
    }
}
