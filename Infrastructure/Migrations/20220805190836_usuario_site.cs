using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class usuario_site : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.AddColumn<string>(
                name: "usuario_site",
                schema: "personal",
                table: "senha",
                type: "varchar(200)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DropColumn(
                name: "usuario_site",
                schema: "personal",
                table: "senha");
        }
    }
}
