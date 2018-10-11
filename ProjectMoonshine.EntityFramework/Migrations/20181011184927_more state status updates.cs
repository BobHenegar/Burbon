using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class morestatestatusupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ct6Code",
                table: "CredentialStatusType",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ct6Code",
                table: "CredentialStatusType",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
