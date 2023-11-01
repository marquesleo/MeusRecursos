using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class tablecategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "usuario_categoria",
                schema: "personal",
                table: "senha",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "categoria",
                schema: "personal",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "varchar(200)", nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    usuario = table.Column<Guid>(type: "uuid", nullable: true),
                    url_img_site = table.Column<string>(type: "text", nullable: true),
                    imagem = table.Column<byte[]>(type: "bytea", nullable: true),
                    nomeimagem = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.id);
                    table.ForeignKey(
                        name: "FK_categoria_usuario_usuario",
                        column: x => x.usuario,
                        principalSchema: "personal",
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categoria_usuario",
                schema: "personal",
                table: "categoria",
                column: "usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categoria",
                schema: "personal");

            migrationBuilder.DropColumn(
                name: "usuario_categoria",
                schema: "personal",
                table: "senha");
        }
    }
}
