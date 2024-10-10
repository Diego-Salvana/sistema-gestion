using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Productos",
                newName: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UsuarioId",
                table: "Productos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Usuarios_UsuarioId",
                table: "Productos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Usuarios_UsuarioId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_UsuarioId",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Productos",
                newName: "IdUsuario");
        }
    }
}
