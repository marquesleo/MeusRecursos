using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fk_restrict_usuario_categoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoria_usuario_usuario",
                schema: "personal",
                table: "categoria");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_atualizacao",
                schema: "personal",
                table: "senha",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Revoked",
                schema: "personal",
                table: "refreshtoken",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                schema: "personal",
                table: "refreshtoken",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "personal",
                table: "refreshtoken",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataDeAcesso",
                schema: "personal",
                table: "contadorsenha",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_usuario_usuario",
                schema: "personal",
                table: "categoria",
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
                name: "FK_categoria_usuario_usuario",
                schema: "personal",
                table: "categoria");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_atualizacao",
                schema: "personal",
                table: "senha",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Revoked",
                schema: "personal",
                table: "refreshtoken",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                schema: "personal",
                table: "refreshtoken",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "personal",
                table: "refreshtoken",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataDeAcesso",
                schema: "personal",
                table: "contadorsenha",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_usuario_usuario",
                schema: "personal",
                table: "categoria",
                column: "usuario",
                principalSchema: "personal",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
