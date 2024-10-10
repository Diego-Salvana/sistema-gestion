using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductosVentas : Migration
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

            migrationBuilder.CreateTable(
                name: "ProductoVenta",
                columns: table => new
                {
                    ProductosId = table.Column<int>(type: "int", nullable: false),
                    VentasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoVenta", x => new { x.ProductosId, x.VentasId });
                    table.ForeignKey(
                        name: "FK_ProductoVenta_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductoVenta_Ventas_VentasId",
                        column: x => x.VentasId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoVenta_VentasId",
                table: "ProductoVenta",
                column: "VentasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoVenta");

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
