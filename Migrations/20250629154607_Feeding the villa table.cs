using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class Feedingthevillatable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villa",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupants", "SquareMeters", "Tariff", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "", new DateOnly(2025, 6, 29), "Detalles de la Villa", "", "Villa Real", 5, 50, 200.0, new DateOnly(2025, 6, 29) },
                    { 2, "", new DateOnly(2025, 6, 29), "Detalles de la Villa", "", "Premium Vista a la Piscina", 4, 40, 150.0, new DateOnly(2025, 6, 29) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
