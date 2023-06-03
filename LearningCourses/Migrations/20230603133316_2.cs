using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningCourses.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePDF",
                schema: "Identity",
                table: "Material",
                newName: "File");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                schema: "Identity",
                table: "Material",
                newName: "FilePDF");
        }
    }
}
