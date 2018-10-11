using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddCredentialStatusTypestoCredentialRequirementTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ct3CompleteCt6Key",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct3MetCt6Key",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct3StartCt6Key",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct3SubmitCt6Key",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3CompleteCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3CompleteCt6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3MetCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3MetCt6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3StartCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3StartCt6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3SubmitCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3SubmitCt6Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3CompleteCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3CompleteCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3MetCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3MetCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3StartCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3StartCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3SubmitCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3SubmitCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3CompleteCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3MetCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3StartCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3SubmitCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3CompleteCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3MetCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3StartCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3SubmitCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3CompleteCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3MetCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3StartCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3SubmitCt6Key",
                table: "CredentialRequirementType");
        }
    }
}
