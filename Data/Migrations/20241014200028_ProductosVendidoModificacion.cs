using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductosVendidoModificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoVendidoVenta");

            migrationBuilder.AddColumn<int>(
                name: "VentaId",
                table: "ProductosVendidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductosVendidos_VentaId",
                table: "ProductosVendidos",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosVendidos_Ventas_VentaId",
                table: "ProductosVendidos",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosVendidos_Ventas_VentaId",
                table: "ProductosVendidos");

            migrationBuilder.DropIndex(
                name: "IX_ProductosVendidos_VentaId",
                table: "ProductosVendidos");

            migrationBuilder.DropColumn(
                name: "VentaId",
                table: "ProductosVendidos");

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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoVendidoVenta_Ventas_VentasId",
                        column: x => x.VentasId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoVendidoVenta_VentasId",
                table: "ProductoVendidoVenta",
                column: "VentasId");
        }
    }
}
