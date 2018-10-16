using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class NormalizedIsPrimaryFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmlIsPrimaryEmail",
                table: "Email",
                newName: "EmlIsPrimary");

            migrationBuilder.RenameColumn(
                name: "AdrIsPrimaryAddress",
                table: "Address",
                newName: "AdrIsPrimary");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmlIsPrimary",
                table: "Email",
                newName: "EmlIsPrimaryEmail");

            migrationBuilder.RenameColumn(
                name: "AdrIsPrimary",
                table: "Address",
                newName: "AdrIsPrimaryAddress");
        }
    }
}
