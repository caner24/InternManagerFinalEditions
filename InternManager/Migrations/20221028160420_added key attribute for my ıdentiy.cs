using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternManager.WebUI.Migrations
{
    public partial class addedkeyattributeformyıdentiy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Persons",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AlterColumn<string>(
                name: "FacultyName",
                table: "Faculties",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FacultyName",
                table: "Faculties",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
