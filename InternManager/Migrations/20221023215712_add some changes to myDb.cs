using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternManager.WebUI.Migrations
{
    public partial class addsomechangestomyDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Ises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Interns2",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Interns1",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecFileEnd",
                table: "Interns",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecFileStart",
                table: "Interns",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsSuper",
                table: "Bosses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Komisyons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<int>(nullable: false),
                    IsSuper = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komisyons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komisyons_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Komisyons_TeacherId",
                table: "Komisyons",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komisyons");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Ises");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Interns2");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Interns1");

            migrationBuilder.DropColumn(
                name: "RecFileEnd",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "RecFileStart",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "IsSuper",
                table: "Bosses");
        }
    }
}
