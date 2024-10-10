using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductosVendidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdVentas",
                table: "ProductosVendidos");

            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "ProductosVendidos",
                newName: "ProductoId");

            migrationBuilder.CreateTable(
                name: "ProductoVendidoVenta",
                columns: table => new
                {
                    ProductoVendidosId = table.Column<int>(type: "int", nullable: false),
                    VentasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoVendidoVenta", x => new { x.ProductoVendidosId, x.VentasId });
                    table.ForeignKey(
                        name: "FK_ProductoVendidoVenta_ProductosVendidos_ProductoVendidosId",
                        column: x => x.ProductoVendidosId,
                        principalTable: "ProductosVendidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductoVendidoVenta_Ventas_VentasId",
                        column: x => x.VentasId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductosVendidos_ProductoId",
                table: "ProductosVendidos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoVendidoVenta_VentasId",
                table: "ProductoVendidoVenta",
                column: "VentasId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosVendidos_Productos_ProductoId",
                table: "ProductosVendidos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosVendidos_Productos_ProductoId",
                table: "ProductosVendidos");

            migrationBuilder.DropTable(
                name: "ProductoVendidoVenta");

            migrationBuilder.DropIndex(
                name: "IX_ProductosVendidos_ProductoId",
                table: "ProductosVendidos");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "ProductosVendidos",
                newName: "IdProducto");

            migrationBuilder.AddColumn<string>(
                name: "IdVentas",
                table: "ProductosVendidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
