using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorEducationSystem.Data.Migrations
{
    public partial class EducationTaskaddId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationTasks_SubjectGroups_SubjectGroupId",
                table: "EducationTasks");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectGroupId",
                table: "EducationTasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationTasks_SubjectGroups_SubjectGroupId",
                table: "EducationTasks",
                column: "SubjectGroupId",
                principalTable: "SubjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationTasks_SubjectGroups_SubjectGroupId",
                table: "EducationTasks");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectGroupId",
                table: "EducationTasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationTasks_SubjectGroups_SubjectGroupId",
                table: "EducationTasks",
                column: "SubjectGroupId",
                principalTable: "SubjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
