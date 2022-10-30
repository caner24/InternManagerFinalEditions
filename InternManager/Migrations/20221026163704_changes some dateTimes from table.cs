using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternManager.WebUI.Migrations
{
    public partial class changessomedateTimesfromtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TotalDays",
                table: "Ises");

            migrationBuilder.DropColumn(
                name: "TotalDays",
                table: "Interns2");

            migrationBuilder.DropColumn(
                name: "TotalDays",
                table: "Interns1");

            migrationBuilder.DropColumn(
                name: "RecEnd2",
                table: "Interns");

            migrationBuilder.DropColumn(
                name: "RecStart2",
                table: "Interns");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Ises",
                newName: "RecStart");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Ises",
                newName: "RecFileStart");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Ises",
                newName: "RecFileEnd");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Interns2",
                newName: "RecStart");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Interns2",
                newName: "RecFileStart");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Interns2",
                newName: "RecFileEnd");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Interns1",
                newName: "RecStart");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Interns1",
                newName: "RecFileStart");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Interns1",
                newName: "RecFileEnd");

            migrationBuilder.AlterColumn<string>(
                name: "OkDays",
                table: "Ises",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Ises",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Ises",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "DetailDocument",
                table: "Ises",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DetailDocument2",
                table: "Ises",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<DateTime>(
                name: "RecEnd",
                table: "Ises",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "OkDays",
                table: "Interns2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Interns2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Interns2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "DetailDocument",
                table: "Interns2",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DetailDocument2",
                table: "Interns2",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<DateTime>(
                name: "RecEnd",
                table: "Interns2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "OkDays",
                table: "Interns1",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Interns1",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Interns1",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "DetailDocument",
                table: "Interns1",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DetailDocument2",
                table: "Interns1",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<DateTime>(
                name: "RecEnd",
                table: "Interns1",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailDocument2",
                table: "Ises");

            migrationBuilder.DropColumn(
                name: "RecEnd",
                table: "Ises");

            migrationBuilder.DropColumn(
                name: "DetailDocument2",
                table: "Interns2");

            migrationBuilder.DropColumn(
                name: "RecEnd",
                table: "Interns2");

            migrationBuilder.DropColumn(
                name: "DetailDocument2",
                table: "Interns1");

            migrationBuilder.DropColumn(
                name: "RecEnd",
                table: "Interns1");

            migrationBuilder.RenameColumn(
                name: "RecStart",
                table: "Ises",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "RecFileStart",
                table: "Ises",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "RecFileEnd",
                table: "Ises",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "RecStart",
                table: "Interns2",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "RecFileStart",
                table: "Interns2",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "RecFileEnd",
                table: "Interns2",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "RecStart",
                table: "Interns1",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "RecFileStart",
                table: "Interns1",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "RecFileEnd",
                table: "Interns1",
                newName: "CreateDate");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Students",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OkDays",
                table: "Ises",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Ises",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Ises",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<byte[]>(
                name: "DetailDocument",
                table: "Ises",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AddColumn<int>(
                name: "TotalDays",
                table: "Ises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "OkDays",
                table: "Interns2",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Interns2",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Interns2",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<byte[]>(
                name: "DetailDocument",
                table: "Interns2",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AddColumn<int>(
                name: "TotalDays",
                table: "Interns2",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "OkDays",
                table: "Interns1",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Interns1",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "Interns1",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<byte[]>(
                name: "DetailDocument",
                table: "Interns1",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AddColumn<int>(
                name: "TotalDays",
                table: "Interns1",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecEnd2",
                table: "Interns",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecStart2",
                table: "Interns",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
