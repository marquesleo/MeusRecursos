using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ColunaDtAcesso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contadorsenha_senha",
                schema: "personal",
                table: "contadorsenha");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeAcesso",
                schema: "personal",
                table: "contadorsenha",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_contadorsenha_senha",
                schema: "personal",
                table: "contadorsenha",
                column: "senha");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contadorsenha_senha",
                schema: "personal",
                table: "contadorsenha");

            migrationBuilder.DropColumn(
                name: "DataDeAcesso",
                schema: "personal",
                table: "contadorsenha");

            migrationBuilder.CreateIndex(
                name: "IX_contadorsenha_senha",
                schema: "personal",
                table: "contadorsenha",
                column: "senha",
                unique: true);
        }
    }
}
