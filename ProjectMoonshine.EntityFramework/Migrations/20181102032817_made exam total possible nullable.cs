using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class madeexamtotalpossiblenullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ExsTotalPossible",
                table: "ExamScores",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ExsTotalPossible",
                table: "ExamScores",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
