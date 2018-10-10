using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class Addcredentialvirtualfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Person_Cr1CusKey",
                table: "Credentials");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Customer_Cr1CusKey",
                table: "Credentials",
                column: "Cr1CusKey",
                principalTable: "Customer",
                principalColumn: "CusKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Customer_Cr1CusKey",
                table: "Credentials");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Person_Cr1CusKey",
                table: "Credentials",
                column: "Cr1CusKey",
                principalTable: "Person",
                principalColumn: "PerKey",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
