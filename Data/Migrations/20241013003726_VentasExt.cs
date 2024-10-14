using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class VentasExt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoVenta");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Ventas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadVendida",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Productos",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VentaId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ProductoId",
                table: "Ventas",
                column: "ProductoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Productos_ProductoId",
                table: "Ventas",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Ventas_VentaId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Productos_ProductoId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_ProductoId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Productos_VentaId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "CantidadVendida",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoVenta_Ventas_VentasId",
                        column: x => x.VentasId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoVenta_VentasId",
                table: "ProductoVenta",
                column: "VentasId");
        }
    }
}
