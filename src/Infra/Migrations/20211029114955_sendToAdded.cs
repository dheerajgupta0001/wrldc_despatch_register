using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class sendToAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SendTo",
                table: "Despatches",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendTo",
                table: "Despatches");
        }
    }
}
