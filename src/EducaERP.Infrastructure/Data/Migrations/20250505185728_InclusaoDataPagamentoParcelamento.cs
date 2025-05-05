using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducaERP.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InclusaoDataPagamentoParcelamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "datapagamento",
                table: "parcelamentos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datapagamento",
                table: "mensalidades",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datapagamento",
                table: "parcelamentos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datapagamento",
                table: "mensalidades",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
