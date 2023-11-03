using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class retirada_usuario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "usuario1",
                schema: "personal",
                table: "senha");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "usuario1",
                schema: "personal",
                table: "senha",
                type: "uuid",
                nullable: true);
        }
    }
}
