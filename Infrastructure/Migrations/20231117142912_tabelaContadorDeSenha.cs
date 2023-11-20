using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class tabelaContadorDeSenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contadorsenha",
                schema: "personal",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    senha = table.Column<Guid>(type: "uuid", nullable: false),
                    contador = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contadorsenha", x => x.id);
                    table.ForeignKey(
                        name: "FK_contadorsenha_senha_senha",
                        column: x => x.senha,
                        principalSchema: "personal",
                        principalTable: "senha",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contadorsenha_senha",
                schema: "personal",
                table: "contadorsenha",
                column: "senha",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contadorsenha",
                schema: "personal");
        }
    }
}
