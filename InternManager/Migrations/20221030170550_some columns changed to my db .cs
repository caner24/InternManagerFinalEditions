using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternManager.WebUI.Migrations
{
    public partial class somecolumnschangedtomydb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecEnd",
                table: "Ises");

            migrationBuilder.DropColumn(
                name: "RecFileEnd",
                table: "Ises");

            migrationBuilder.DropColumn(
                name: "RecFileStart",
                table: "Ises");

            migrationBuilder.DropColumn(
                name: "RecStart",
                table: "Ises");

            migrationBuilder.DropColumn(
                name: "RecEnd",
                table: "Interns2");

            migrationBuilder.DropColumn(
                name: "RecFileEnd",
                table: "Interns2");

            migrationBuilder.DropColumn(
                name: "RecFileStart",
                table: "Interns2");

            migrationBuilder.DropColumn(
                name: "RecStart",
                table: "Interns2");

            migrationBuilder.DropColumn(
                name: "RecEnd",
                table: "Interns1");

            migrationBuilder.DropColumn(
                name: "RecFileEnd",
                table: "Interns1");

            migrationBuilder.DropColumn(
                name: "RecFileStart",
                table: "Interns1");

            migrationBuilder.DropColumn(
                name: "RecStart",
                table: "Interns1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RecEnd",
                table: "Ises",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecFileEnd",
                table: "Ises",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecFileStart",
                table: "Ises",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecStart",
                table: "Ises",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecEnd",
                table: "Interns2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecFileEnd",
                table: "Interns2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecFileStart",
                table: "Interns2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecStart",
                table: "Interns2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecEnd",
                table: "Interns1",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecFileEnd",
                table: "Interns1",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecFileStart",
                table: "Interns1",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecStart",
                table: "Interns1",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
