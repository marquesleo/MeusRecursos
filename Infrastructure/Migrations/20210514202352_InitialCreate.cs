using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "personal");

            migrationBuilder.CreateTable(
                name: "usuario",
                schema: "personal",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    username = table.Column<string>(type: "varchar(50)", nullable: true),
                    password = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prioridade",
                schema: "personal",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    descricao = table.Column<string>(type: "varchar(200)", nullable: true),
                    valor = table.Column<int>(nullable: false),
                    ativo = table.Column<bool>(nullable: false),
                    usuario = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prioridade", x => x.id);
                    table.ForeignKey(
                        name: "FK_prioridade_usuario_usuario",
                        column: x => x.usuario,
                        principalSchema: "personal",
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_prioridade_usuario",
                schema: "personal",
                table: "prioridade",
                column: "usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prioridade",
                schema: "personal");

            migrationBuilder.DropTable(
                name: "usuario",
                schema: "personal");
        }
    }
}
