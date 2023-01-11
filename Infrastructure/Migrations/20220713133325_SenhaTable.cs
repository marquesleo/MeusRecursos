using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class SenhaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "senha",
                schema: "personal",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "varchar(200)", nullable: true),
                    observacao = table.Column<string>(type: "text", nullable: true),
                    site = table.Column<string>(type: "varchar(500)", nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    password = table.Column<string>(type: "varchar(500)", nullable: true),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    url_img_site = table.Column<string>(type: "text", nullable: true),
                    usuario = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_senha", x => x.id);
                    table.ForeignKey(
                        name: "FK_senha_usuario_usuario",
                        column: x => x.usuario,
                        principalSchema: "personal",
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_senha_usuario",
                schema: "personal",
                table: "senha",
                column: "usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "senha",
                schema: "personal");
        }
    }
}
