using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedqueryruletorequirementtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ct3QryKey",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3QryKey",
                table: "CredentialRequirementType",
                column: "Ct3QryKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_Queries_Ct3QryKey",
                table: "CredentialRequirementType",
                column: "Ct3QryKey",
                principalTable: "Queries",
                principalColumn: "QryKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_Queries_Ct3QryKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3QryKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3QryKey",
                table: "CredentialRequirementType");
        }
    }
}
