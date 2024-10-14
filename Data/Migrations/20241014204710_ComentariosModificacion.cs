using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ComentariosModificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.AddColumn<string>(
                name: "Comentario",
                table: "Ventas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comentario",
                table: "Ventas");

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VentaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentario_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_VentaId",
                table: "Comentario",
                column: "VentaId");
        }
    }
}
