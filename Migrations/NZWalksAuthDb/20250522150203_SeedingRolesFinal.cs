using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace walks.Migrations.NZWalksAuthDb
{
    /// <inheritdoc />
    public partial class SeedingRolesFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cd6b67e-fb40-44d9-9df4-1a2435c93325");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6ca895f-43ed-4c75-a985-c48f3a1e1e05");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1cc5f2bc-ff4b-47c0-a423-1add56c6497e", "1cc5f2bc-ff4b-47c0-a423-1add56c6497e", "Writer", "WRITER" },
                    { "1cc5f2bc-ff4b-47c0-a475-1add56c6497d", "1cc5f2bc-ff4b-47c0-a475-1add56c6497d", "Reader", "READER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cc5f2bc-ff4b-47c0-a423-1add56c6497e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cc5f2bc-ff4b-47c0-a475-1add56c6497d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9cd6b67e-fb40-44d9-9df4-1a2435c93325", "0f83c98b-87be-46b7-92a5-9122e1758b1a", "Reader", "READER" },
                    { "d6ca895f-43ed-4c75-a985-c48f3a1e1e05", "6ea8c2df-955e-4c5b-b573-0fac7df2bbd0", "Writer", "WRITER" }
                });
        }
    }
}
