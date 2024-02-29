using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CategoriaIdColumn_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "categoria",
                schema: "personal",
                table: "senha",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_senha_categoria",
                schema: "personal",
                table: "senha",
                column: "categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_senha_categoria_categoria",
                schema: "personal",
                table: "senha",
                column: "categoria",
                principalSchema: "personal",
                principalTable: "categoria",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_senha_categoria_categoria",
                schema: "personal",
                table: "senha");

            migrationBuilder.DropIndex(
                name: "IX_senha_categoria",
                schema: "personal",
                table: "senha");

            migrationBuilder.DropColumn(
                name: "categoria",
                schema: "personal",
                table: "senha");
        }
    }
}
