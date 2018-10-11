using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedstatustoCredentialProcessProcesstype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Cr3Ct6Key",
                table: "CredentialRequirement",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Ct2CompletedCt6Key",
                table: "CredentialProcessType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct2StartCt6Key",
                table: "CredentialProcessType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct2SubmittedCt6Key",
                table: "CredentialProcessType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct6SubmittedCt6Key",
                table: "CredentialProcessType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Cr2Ct6Key",
                table: "CredentialProcess",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirement_Cr3Ct6Key",
                table: "CredentialRequirement",
                column: "Cr3Ct6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcessType_Ct2CompletedCt6Key",
                table: "CredentialProcessType",
                column: "Ct2CompletedCt6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcessType_Ct2StartCt6Key",
                table: "CredentialProcessType",
                column: "Ct2StartCt6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcessType_Ct2SubmittedCt6Key",
                table: "CredentialProcessType",
                column: "Ct2SubmittedCt6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcess_Cr2Ct6Key",
                table: "CredentialProcess",
                column: "Cr2Ct6Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcess_CredentialStatusType_Cr2Ct6Key",
                table: "CredentialProcess",
                column: "Cr2Ct6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcessType_CredentialStatusType_Ct2CompletedCt6Key",
                table: "CredentialProcessType",
                column: "Ct2CompletedCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcessType_CredentialStatusType_Ct2StartCt6Key",
                table: "CredentialProcessType",
                column: "Ct2StartCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcessType_CredentialStatusType_Ct2SubmittedCt6Key",
                table: "CredentialProcessType",
                column: "Ct2SubmittedCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirement_CredentialStatusType_Cr3Ct6Key",
                table: "CredentialRequirement",
                column: "Cr3Ct6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcess_CredentialStatusType_Cr2Ct6Key",
                table: "CredentialProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcessType_CredentialStatusType_Ct2CompletedCt6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcessType_CredentialStatusType_Ct2StartCt6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcessType_CredentialStatusType_Ct2SubmittedCt6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirement_CredentialStatusType_Cr3Ct6Key",
                table: "CredentialRequirement");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirement_Cr3Ct6Key",
                table: "CredentialRequirement");

            migrationBuilder.DropIndex(
                name: "IX_CredentialProcessType_Ct2CompletedCt6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialProcessType_Ct2StartCt6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialProcessType_Ct2SubmittedCt6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialProcess_Cr2Ct6Key",
                table: "CredentialProcess");

            migrationBuilder.DropColumn(
                name: "Cr3Ct6Key",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "Ct2CompletedCt6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Ct2StartCt6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Ct2SubmittedCt6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Ct6SubmittedCt6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Cr2Ct6Key",
                table: "CredentialProcess");
        }
    }
}
