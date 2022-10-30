using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternManager.WebUI.Migrations
{
    public partial class addsomechangesforKURUMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interns1_Kurums_KurumId",
                table: "Interns1");

            migrationBuilder.DropForeignKey(
                name: "FK_Interns2_Kurums_KurumId",
                table: "Interns2");

            migrationBuilder.DropForeignKey(
                name: "FK_Ises_Kurums_KurumId",
                table: "Ises");

            migrationBuilder.DropTable(
                name: "Kurums");

            migrationBuilder.DropIndex(
                name: "IX_Ises_KurumId",
                table: "Ises");

            migrationBuilder.DropIndex(
                name: "IX_Interns2_KurumId",
                table: "Interns2");

            migrationBuilder.DropIndex(
                name: "IX_Interns1_KurumId",
                table: "Interns1");

            migrationBuilder.DropColumn(
                name: "KurumId",
                table: "Ises");

            migrationBuilder.DropColumn(
                name: "KurumId",
                table: "Interns2");

            migrationBuilder.DropColumn(
                name: "KurumId",
                table: "Interns1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KurumId",
                table: "Ises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KurumId",
                table: "Interns2",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KurumId",
                table: "Interns1",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Kurums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adress = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    DevletKatkisi = table.Column<bool>(nullable: false),
                    FaaliyetAlani = table.Column<string>(nullable: false),
                    Fax = table.Column<string>(nullable: true),
                    FirmaYetkiliAdSoyad = table.Column<string>(nullable: false),
                    KurumMail = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: false),
                    ResmiAd = table.Column<string>(nullable: false),
                    StajSorumlusu = table.Column<string>(nullable: false),
                    TelNo = table.Column<string>(nullable: true),
                    TelNo2 = table.Column<string>(nullable: true),
                    Town = table.Column<string>(nullable: false),
                    Unvan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurums", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ises_KurumId",
                table: "Ises",
                column: "KurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Interns2_KurumId",
                table: "Interns2",
                column: "KurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Interns1_KurumId",
                table: "Interns1",
                column: "KurumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interns1_Kurums_KurumId",
                table: "Interns1",
                column: "KurumId",
                principalTable: "Kurums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interns2_Kurums_KurumId",
                table: "Interns2",
                column: "KurumId",
                principalTable: "Kurums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ises_Kurums_KurumId",
                table: "Ises",
                column: "KurumId",
                principalTable: "Kurums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
