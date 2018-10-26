using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedcompleteandunderreviewflagstostatusstatemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cu6CompletedFlag",
                table: "StatusStateType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Cu6UnderReviewFlag",
                table: "StatusStateType",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cu6CompletedFlag",
                table: "StatusStateType");

            migrationBuilder.DropColumn(
                name: "Cu6UnderReviewFlag",
                table: "StatusStateType");
        }
    }
}
