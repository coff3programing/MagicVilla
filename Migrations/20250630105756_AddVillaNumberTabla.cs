using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumberTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "villaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_villaNumbers", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_villaNumbers_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateOnly(2025, 6, 30), new DateOnly(2025, 6, 30) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateOnly(2025, 6, 30), new DateOnly(2025, 6, 30) });

            migrationBuilder.CreateIndex(
                name: "IX_villaNumbers_VillaId",
                table: "villaNumbers",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "villaNumbers");

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateOnly(2025, 6, 29), new DateOnly(2025, 6, 29) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdateDate" },
                values: new object[] { new DateOnly(2025, 6, 29), new DateOnly(2025, 6, 29) });
        }
    }
}
