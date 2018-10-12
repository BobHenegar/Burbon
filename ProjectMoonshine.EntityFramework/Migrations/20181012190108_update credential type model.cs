using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class updatecredentialtypemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CredentialType_Ct1RenewCt2Key",
                table: "CredentialType",
                column: "Ct1RenewCt2Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialType_CredentialProcessType_Ct1RenewCt2Key",
                table: "CredentialType",
                column: "Ct1RenewCt2Key",
                principalTable: "CredentialProcessType",
                principalColumn: "Ct2Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialType_CredentialProcessType_Ct1RenewCt2Key",
                table: "CredentialType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialType_Ct1RenewCt2Key",
                table: "CredentialType");
        }
    }
}
