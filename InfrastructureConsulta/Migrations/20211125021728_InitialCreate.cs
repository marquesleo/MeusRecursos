using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Consulta.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "consultapsi");

            migrationBuilder.CreateTable(
                name: "empresa",
                schema: "consultapsi",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cnpj = table.Column<string>(type: "varchar(18)", nullable: true),
                    nome = table.Column<string>(type: "varchar(200)", nullable: true),
                    email = table.Column<string>(type: "varchar(200)", nullable: true),
                    celular = table.Column<string>(type: "varchar(14)", nullable: true),
                    telefone = table.Column<string>(type: "varchar(14)", nullable: true),
                    endereco = table.Column<string>(type: "varchar(200)", nullable: true),
                    cep = table.Column<string>(type: "varchar(20)", nullable: true),
                    cidade = table.Column<string>(type: "varchar(150)", nullable: true),
                    bairro = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                schema: "consultapsi",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "varchar(50)", nullable: true),
                    email = table.Column<string>(type: "varchar(200)", nullable: true),
                    password = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "acesso",
                schema: "consultapsi",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acesso", x => x.id);
                    table.ForeignKey(
                        name: "FK_acesso_empresa_empresa_id",
                        column: x => x.empresa_id,
                        principalSchema: "consultapsi",
                        principalTable: "empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acesso_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalSchema: "consultapsi",
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "paciente",
                schema: "consultapsi",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    dt_nascimento = table.Column<DateTime>(type: "Date", nullable: false),
                    acesso_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "varchar(200)", nullable: true),
                    email = table.Column<string>(type: "varchar(200)", nullable: true),
                    celular = table.Column<string>(type: "varchar(14)", nullable: true),
                    telefone = table.Column<string>(type: "varchar(14)", nullable: true),
                    endereco = table.Column<string>(type: "varchar(200)", nullable: true),
                    cep = table.Column<string>(type: "varchar(20)", nullable: true),
                    cidade = table.Column<string>(type: "varchar(150)", nullable: true),
                    bairro = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paciente", x => x.id);
                    table.ForeignKey(
                        name: "FK_paciente_acesso_acesso_id",
                        column: x => x.acesso_id,
                        principalSchema: "consultapsi",
                        principalTable: "acesso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "psicologa",
                schema: "consultapsi",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    dt_nascimento = table.Column<DateTime>(type: "Date", nullable: false),
                    crp = table.Column<string>(type: "varchar(20)", nullable: true),
                    acesso_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "varchar(200)", nullable: true),
                    email = table.Column<string>(type: "varchar(200)", nullable: true),
                    celular = table.Column<string>(type: "varchar(14)", nullable: true),
                    telefone = table.Column<string>(type: "varchar(14)", nullable: true),
                    endereco = table.Column<string>(type: "varchar(200)", nullable: true),
                    cep = table.Column<string>(type: "varchar(20)", nullable: true),
                    cidade = table.Column<string>(type: "varchar(150)", nullable: true),
                    bairro = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_psicologa", x => x.id);
                    table.ForeignKey(
                        name: "FK_psicologa_acesso_acesso_id",
                        column: x => x.acesso_id,
                        principalSchema: "consultapsi",
                        principalTable: "acesso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consulta",
                schema: "consultapsi",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    horario = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    proposito = table.Column<string>(type: "varchar(50)", nullable: true),
                    umahora = table.Column<bool>(type: "boolean", nullable: false),
                    observacao = table.Column<string>(type: "varchar(500)", nullable: true),
                    status = table.Column<string>(type: "char(1)", nullable: true),
                    satisfacao = table.Column<int>(type: "int", nullable: false),
                    comentario = table.Column<string>(type: "varchar(500)", nullable: true),
                    horariosatisfacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    javiusatisfacao = table.Column<bool>(type: "boolean", nullable: false),
                    psicologa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    paciente_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consulta", x => x.id);
                    table.ForeignKey(
                        name: "FK_consulta_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalSchema: "consultapsi",
                        principalTable: "paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_consulta_psicologa_psicologa_id",
                        column: x => x.psicologa_id,
                        principalSchema: "consultapsi",
                        principalTable: "psicologa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_acesso_empresa_id",
                schema: "consultapsi",
                table: "acesso",
                column: "empresa_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_acesso_usuario_id",
                schema: "consultapsi",
                table: "acesso",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_consulta_paciente_id",
                schema: "consultapsi",
                table: "consulta",
                column: "paciente_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_consulta_psicologa_id",
                schema: "consultapsi",
                table: "consulta",
                column: "psicologa_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_paciente_acesso_id",
                schema: "consultapsi",
                table: "paciente",
                column: "acesso_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_psicologa_acesso_id",
                schema: "consultapsi",
                table: "psicologa",
                column: "acesso_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consulta",
                schema: "consultapsi");

            migrationBuilder.DropTable(
                name: "paciente",
                schema: "consultapsi");

            migrationBuilder.DropTable(
                name: "psicologa",
                schema: "consultapsi");

            migrationBuilder.DropTable(
                name: "acesso",
                schema: "consultapsi");

            migrationBuilder.DropTable(
                name: "empresa",
                schema: "consultapsi");

            migrationBuilder.DropTable(
                name: "usuario",
                schema: "consultapsi");
        }
    }
}
