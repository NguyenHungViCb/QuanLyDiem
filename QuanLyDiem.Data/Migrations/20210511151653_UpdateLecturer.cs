using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyDiem.Data.Migrations
{
    public partial class UpdateLecturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Lecturers");
        }
    }
}
