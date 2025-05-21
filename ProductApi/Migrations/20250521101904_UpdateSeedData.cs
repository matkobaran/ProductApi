using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 7, "", "www.alza.cz", "Product #7", 87m, 0 },
                    { 8, "", "www.alza.cz", "Product #8", 88m, 0 },
                    { 9, "", "www.alza.cz", "Product #9", 89m, 0 },
                    { 10, "", "www.alza.cz", "Product #10", 90m, 0 },
                    { 11, "", "www.alza.cz", "Product #11", 91m, 0 },
                    { 12, "", "www.alza.cz", "Product #12", 92m, 0 },
                    { 13, "", "www.alza.cz", "Product #13", 93m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 13);
        }
    }
}
