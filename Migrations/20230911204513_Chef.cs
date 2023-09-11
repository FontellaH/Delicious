using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delicious.Migrations
{
    public partial class Chef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChefName",
                table: "Dishs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChefName",
                table: "Dishs");
        }
    }
}
