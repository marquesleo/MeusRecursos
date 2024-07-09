using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fk_restrict_chaves_estrangeiras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_senha_usuario_usuario",
                schema: "personal",
                table: "senha");

            migrationBuilder.AddForeignKey(
                name: "FK_senha_usuario_usuario",
                schema: "personal",
                table: "senha",
                column: "usuario",
                principalSchema: "personal",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_senha_usuario_usuario",
                schema: "personal",
                table: "senha");

            migrationBuilder.AddForeignKey(
                name: "FK_senha_usuario_usuario",
                schema: "personal",
                table: "senha",
                column: "usuario",
                principalSchema: "personal",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
