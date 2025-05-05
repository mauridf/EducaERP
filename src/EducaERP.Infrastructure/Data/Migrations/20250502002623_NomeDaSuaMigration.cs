using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducaERP.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaSuaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservas_livros_livroid",
                table: "reservas");

            migrationBuilder.AddForeignKey(
                name: "FK_reservas_livros_livroid",
                table: "reservas",
                column: "livroid",
                principalTable: "livros",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservas_livros_livroid",
                table: "reservas");

            migrationBuilder.AddForeignKey(
                name: "FK_reservas_livros_livroid",
                table: "reservas",
                column: "livroid",
                principalTable: "livros",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
