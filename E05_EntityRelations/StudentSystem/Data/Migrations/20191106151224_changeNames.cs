using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_StudentSystem.Data.Migrations
{
    public partial class changeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_Courses_CourseId",
                table: "HomeWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_Students_StudentId",
                table: "HomeWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_Courses_CourseId",
                table: "StudentsCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_Students_StudentId",
                table: "StudentsCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsCourses",
                table: "StudentsCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeWorks",
                table: "HomeWorks");

            migrationBuilder.RenameTable(
                name: "StudentsCourses",
                newName: "StudentCourses");

            migrationBuilder.RenameTable(
                name: "HomeWorks",
                newName: "HomeworkSubmissions");

            migrationBuilder.RenameColumn(
                name: "RegisterOn",
                table: "Students",
                newName: "RegisteredOn");

            migrationBuilder.RenameIndex(
                name: "IX_StudentsCourses_StudentId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_HomeWorks_StudentId",
                table: "HomeworkSubmissions",
                newName: "IX_HomeworkSubmissions_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_HomeWorks_CourseId",
                table: "HomeworkSubmissions",
                newName: "IX_HomeworkSubmissions_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses",
                columns: new[] { "CourseId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeworkSubmissions",
                table: "HomeworkSubmissions",
                column: "HomeworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeworkSubmissions_Courses_CourseId",
                table: "HomeworkSubmissions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeworkSubmissions_Students_StudentId",
                table: "HomeworkSubmissions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeworkSubmissions_Courses_CourseId",
                table: "HomeworkSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeworkSubmissions_Students_StudentId",
                table: "HomeworkSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeworkSubmissions",
                table: "HomeworkSubmissions");

            migrationBuilder.RenameTable(
                name: "StudentCourses",
                newName: "StudentsCourses");

            migrationBuilder.RenameTable(
                name: "HomeworkSubmissions",
                newName: "HomeWorks");

            migrationBuilder.RenameColumn(
                name: "RegisteredOn",
                table: "Students",
                newName: "RegisterOn");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_StudentId",
                table: "StudentsCourses",
                newName: "IX_StudentsCourses_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_HomeworkSubmissions_StudentId",
                table: "HomeWorks",
                newName: "IX_HomeWorks_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_HomeworkSubmissions_CourseId",
                table: "HomeWorks",
                newName: "IX_HomeWorks_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsCourses",
                table: "StudentsCourses",
                columns: new[] { "CourseId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeWorks",
                table: "HomeWorks",
                column: "HomeworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_Courses_CourseId",
                table: "HomeWorks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_Students_StudentId",
                table: "HomeWorks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_Courses_CourseId",
                table: "StudentsCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_Students_StudentId",
                table: "StudentsCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
