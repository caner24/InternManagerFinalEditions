using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternManager.WebUI.Migrations
{
    public partial class efCodeFirstdatabaserecreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacultyNumber = table.Column<int>(nullable: false),
                    FacultyName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    RecStart = table.Column<DateTime>(nullable: false),
                    RecEnd = table.Column<DateTime>(nullable: false),
                    RecStart2 = table.Column<DateTime>(nullable: false),
                    RecEnd2 = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kurums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResmiAd = table.Column<string>(nullable: false),
                    FaaliyetAlani = table.Column<string>(nullable: false),
                    Adress = table.Column<string>(nullable: false),
                    Town = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    StajSorumlusu = table.Column<string>(nullable: false),
                    DevletKatkisi = table.Column<bool>(nullable: false),
                    FirmaYetkiliAdSoyad = table.Column<string>(nullable: false),
                    TelNo = table.Column<string>(nullable: true),
                    TelNo2 = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    KurumMail = table.Column<string>(nullable: true),
                    Unvan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdentyNumber = table.Column<string>(nullable: true),
                    Civilization = table.Column<string>(nullable: true),
                    NameSurname = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    Gender = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacultyId = table.Column<int>(nullable: false),
                    StudentNumber = table.Column<string>(nullable: true),
                    StudentPassword = table.Column<string>(nullable: true),
                    IsFirstPassword = table.Column<bool>(nullable: false),
                    StudentMail = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Town = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacultyNumber = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    TeacherNumber = table.Column<string>(nullable: true),
                    TeacherPassword = table.Column<string>(nullable: true),
                    IsBoos = table.Column<bool>(nullable: false),
                    IsFirstPassword = table.Column<bool>(nullable: false),
                    TeacherMail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Faculties_FacultyNumber",
                        column: x => x.FacultyNumber,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interns1",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Student_Id = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    KurumId = table.Column<int>(nullable: false),
                    IsOk = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    TotalDays = table.Column<int>(nullable: false),
                    OkDays = table.Column<string>(nullable: true),
                    InternId = table.Column<int>(nullable: false),
                    DetailDocument = table.Column<byte[]>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interns1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interns1_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interns1_Kurums_KurumId",
                        column: x => x.KurumId,
                        principalTable: "Kurums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interns1_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interns2",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DetailDocument = table.Column<byte[]>(nullable: true),
                    Student_Id = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsOk = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    OkDays = table.Column<string>(nullable: true),
                    TotalDays = table.Column<int>(nullable: false),
                    InternId = table.Column<int>(nullable: false),
                    KurumId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interns2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interns2_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interns2_Kurums_KurumId",
                        column: x => x.KurumId,
                        principalTable: "Kurums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interns2_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DetailDocument = table.Column<byte[]>(nullable: true),
                    Student_Id = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsOk = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    TotalDays = table.Column<int>(nullable: false),
                    OkDays = table.Column<string>(nullable: true),
                    KurumId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    InternId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ises_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ises_Kurums_KurumId",
                        column: x => x.KurumId,
                        principalTable: "Kurums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ises_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bosses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsSuper = table.Column<bool>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bosses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bosses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bosses_TeacherId",
                table: "Bosses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Interns1_InternId",
                table: "Interns1",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_Interns1_KurumId",
                table: "Interns1",
                column: "KurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Interns1_Student_Id",
                table: "Interns1",
                column: "Student_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Interns2_InternId",
                table: "Interns2",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_Interns2_KurumId",
                table: "Interns2",
                column: "KurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Interns2_Student_Id",
                table: "Interns2",
                column: "Student_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ises_InternId",
                table: "Ises",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_Ises_KurumId",
                table: "Ises",
                column: "KurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Ises_Student_Id",
                table: "Ises",
                column: "Student_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PersonId",
                table: "Students",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_FacultyNumber",
                table: "Teachers",
                column: "FacultyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_PersonId",
                table: "Teachers",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bosses");

            migrationBuilder.DropTable(
                name: "Interns1");

            migrationBuilder.DropTable(
                name: "Interns2");

            migrationBuilder.DropTable(
                name: "Ises");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Interns");

            migrationBuilder.DropTable(
                name: "Kurums");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
