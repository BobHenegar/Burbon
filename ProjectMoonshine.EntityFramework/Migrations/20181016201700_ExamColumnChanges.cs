using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class ExamColumnChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Exams_ExmEvtKey",
                table: "Exams");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExmEvtKey",
                table: "Exams",
                column: "ExmEvtKey",
                unique: true,
                filter: "[ExmEvtKey] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Exams_ExmEvtKey",
                table: "Exams");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExmEvtKey",
                table: "Exams",
                column: "ExmEvtKey");
        }
    }
}
