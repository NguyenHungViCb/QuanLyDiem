using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyDiem.Data.Migrations
{
    public partial class classIdToClassId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_classId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "classId",
                table: "Students",
                newName: "ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_classId",
                table: "Students",
                newName: "IX_Students_ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "Students",
                newName: "classId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                newName: "IX_Students_classId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_classId",
                table: "Students",
                column: "classId",
                principalTable: "Classes",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
