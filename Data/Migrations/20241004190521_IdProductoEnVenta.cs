using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class IdProductoEnVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Ventas_VentaId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_VentaId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "VentaId",
                table: "Productos");

            migrationBuilder.AddColumn<string>(
                name: "IdProducto",
                table: "Ventas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProducto",
                table: "Ventas");

            migrationBuilder.AddColumn<int>(
                name: "VentaId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_VentaId",
                table: "Productos",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Ventas_VentaId",
                table: "Productos",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "Id");
        }
    }
}
