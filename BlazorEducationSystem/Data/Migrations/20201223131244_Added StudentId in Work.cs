using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorEducationSystem.Data.Migrations
{
    public partial class AddedStudentIdinWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_StudentInfos_StudentEducationInfoId",
                table: "Works");

            migrationBuilder.RenameColumn(
                name: "StudentEducationInfoId",
                table: "Works",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Works_StudentEducationInfoId",
                table: "Works",
                newName: "IX_Works_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_StudentInfos_StudentId",
                table: "Works",
                column: "StudentId",
                principalTable: "StudentInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_StudentInfos_StudentId",
                table: "Works");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Works",
                newName: "StudentEducationInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Works_StudentId",
                table: "Works",
                newName: "IX_Works_StudentEducationInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_StudentInfos_StudentEducationInfoId",
                table: "Works",
                column: "StudentEducationInfoId",
                principalTable: "StudentInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
