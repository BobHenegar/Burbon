using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class movedexamseatlimittoeventregistrationlimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExmSeatCount",
                table: "Exams");

            migrationBuilder.AddColumn<int>(
                name: "EvtRegistrantLimit",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvtRegistrantLimit",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "ExmSeatCount",
                table: "Exams",
                nullable: true);
        }
    }
}
