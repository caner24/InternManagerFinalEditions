using Microsoft.EntityFrameworkCore.Migrations;

namespace InternManager.WebUI.Migrations
{
    public partial class addedsomedbtablesinmymigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IMEOK",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Dönem",
                table: "Ises",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMEOK",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Dönem",
                table: "Ises");
        }
    }
}
