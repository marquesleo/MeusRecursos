using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class colunanomeImagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "imagem",
                schema: "personal",
                table: "senha",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nomeimagem",
                schema: "personal",
                table: "senha",
                type: "varchar(200)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagem",
                schema: "personal",
                table: "senha");

            migrationBuilder.DropColumn(
                name: "nomeimagem",
                schema: "personal",
                table: "senha");
        }
    }
}
