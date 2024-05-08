using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class acertoUsuarioId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prioridade_usuario_usuario",
                schema: "personal",
                table: "prioridade");

            migrationBuilder.AlterColumn<Guid>(
                name: "usuario",
                schema: "personal",
                table: "prioridade",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_prioridade_usuario_usuario",
                schema: "personal",
                table: "prioridade",
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
                name: "FK_prioridade_usuario_usuario",
                schema: "personal",
                table: "prioridade");

            migrationBuilder.AlterColumn<Guid>(
                name: "usuario",
                schema: "personal",
                table: "prioridade",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_prioridade_usuario_usuario",
                schema: "personal",
                table: "prioridade",
                column: "usuario",
                principalSchema: "personal",
                principalTable: "usuario",
                principalColumn: "id");
        }
    }
}
