using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class updatedCEmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Ct3CEUnitsRequired",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct3CetKey",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3CetKey",
                table: "CredentialRequirementType",
                column: "Ct3CetKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_CEType_Ct3CetKey",
                table: "CredentialRequirementType",
                column: "Ct3CetKey",
                principalTable: "CEType",
                principalColumn: "CetKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_CEType_Ct3CetKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3CetKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3CEUnitsRequired",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3CetKey",
                table: "CredentialRequirementType");
        }
    }
}
