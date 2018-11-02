using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedforeignkeyforct3deniedct6key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3DeniedCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3DeniedCt6Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3DeniedCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3DeniedCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3DeniedCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3DeniedCt6Key",
                table: "CredentialRequirementType");
        }
    }
}
