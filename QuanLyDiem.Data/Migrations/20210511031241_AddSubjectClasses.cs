using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyDiem.Data.Migrations
{
    public partial class AddSubjectClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    LecturerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.LecturerId);
                });

            migrationBuilder.CreateTable(
                name: "SubjectClasses",
                columns: table => new
                {
                    SubjectClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    LecturerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectClasses", x => x.SubjectClassId);
                    table.ForeignKey(
                        name: "FK_SubjectClasses_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "LecturerId");
                    table.ForeignKey(
                        name: "FK_SubjectClasses_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                });

            migrationBuilder.CreateTable(
                name: "SubjectClassDetails",
                columns: table => new
                {
                    SubjectClassDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectClassId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectClassDetails", x => x.SubjectClassDetailId);
                    table.ForeignKey(
                        name: "FK_SubjectClassDetails_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                    table.ForeignKey(
                        name: "FK_SubjectClassDetails_SubjectClasses_SubjectClassId",
                        column: x => x.SubjectClassId,
                        principalTable: "SubjectClasses",
                        principalColumn: "SubjectClassId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClassDetails_StudentId",
                table: "SubjectClassDetails",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClassDetails_SubjectClassId",
                table: "SubjectClassDetails",
                column: "SubjectClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClasses_LecturerId",
                table: "SubjectClasses",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClasses_SubjectId",
                table: "SubjectClasses",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectClassDetails");

            migrationBuilder.DropTable(
                name: "SubjectClasses");

            migrationBuilder.DropTable(
                name: "Lecturers");
        }
    }
}
