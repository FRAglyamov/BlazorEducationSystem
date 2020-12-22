using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorEducationSystem.Data.Migrations
{
    public partial class NameforSubjectGroupFIOforStudentandTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Middlename",
                table: "TeacherInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TeacherInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "TeacherInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SubjectGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Middlename",
                table: "StudentInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StudentInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "StudentInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Middlename",
                table: "TeacherInfos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TeacherInfos");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "TeacherInfos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SubjectGroups");

            migrationBuilder.DropColumn(
                name: "Middlename",
                table: "StudentInfos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "StudentInfos");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "StudentInfos");
        }
    }
}
