using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedparentdependentrequirementrelationshipsandupdatestoothercredentialingrelatedmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cu6MetFlag",
                table: "StatusStateType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct3MetCr2Ct6Key",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ct3SubmitProcessFlag",
                table: "CredentialRequirementType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Cr3Cr3Key",
                table: "CredentialRequirement",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr2SubmitDate",
                table: "CredentialProcess",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3MetCr2Ct6Key",
                table: "CredentialRequirementType",
                column: "Ct3MetCr2Ct6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirement_Cr3Cr3Key",
                table: "CredentialRequirement",
                column: "Cr3Cr3Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirement_CredentialRequirement_Cr3Cr3Key",
                table: "CredentialRequirement",
                column: "Cr3Cr3Key",
                principalTable: "CredentialRequirement",
                principalColumn: "Cr3Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3MetCr2Ct6Key",
                table: "CredentialRequirementType",
                column: "Ct3MetCr2Ct6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirement_CredentialRequirement_Cr3Cr3Key",
                table: "CredentialRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3MetCr2Ct6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3MetCr2Ct6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirement_Cr3Cr3Key",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "Cu6MetFlag",
                table: "StatusStateType");

            migrationBuilder.DropColumn(
                name: "Ct3MetCr2Ct6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3SubmitProcessFlag",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Cr3Cr3Key",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "Cr2SubmitDate",
                table: "CredentialProcess");
        }
    }
}
