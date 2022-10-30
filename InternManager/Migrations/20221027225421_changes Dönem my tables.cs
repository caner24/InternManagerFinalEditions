using Microsoft.EntityFrameworkCore.Migrations;

namespace InternManager.WebUI.Migrations
{
    public partial class changesDönemmytables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dönem",
                table: "Ises");

            migrationBuilder.AddColumn<string>(
                name: "Dönem",
                table: "Interns",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dönem",
                table: "Interns");

            migrationBuilder.AddColumn<string>(
                name: "Dönem",
                table: "Ises",
                nullable: false,
                defaultValue: "");
        }
    }
}
