using Microsoft.EntityFrameworkCore.Migrations;

namespace InternManager.WebUI.Migrations
{
    public partial class addedsomecolomuns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOk2",
                table: "Ises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOk2",
                table: "Interns2",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOk2",
                table: "Interns1",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOk2",
                table: "Ises");

            migrationBuilder.DropColumn(
                name: "IsOk2",
                table: "Interns2");

            migrationBuilder.DropColumn(
                name: "IsOk2",
                table: "Interns1");
        }
    }
}
