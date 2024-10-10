using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class IdVentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdVenta",
                table: "ProductosVendidos");

            migrationBuilder.AddColumn<string>(
                name: "IdVentas",
                table: "ProductosVendidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdVentas",
                table: "ProductosVendidos");

            migrationBuilder.AddColumn<int>(
                name: "IdVenta",
                table: "ProductosVendidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
