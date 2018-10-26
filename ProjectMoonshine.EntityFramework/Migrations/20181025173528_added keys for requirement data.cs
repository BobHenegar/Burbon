using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedkeysforrequirementdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Cr3Ce2Key",
                table: "CredentialRequirement",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Cr3DegKey",
                table: "CredentialRequirement",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Cr3ExsKey",
                table: "CredentialRequirement",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirement_Cr3Ce2Key",
                table: "CredentialRequirement",
                column: "Cr3Ce2Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirement_Cr3DegKey",
                table: "CredentialRequirement",
                column: "Cr3DegKey");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirement_Cr3ExsKey",
                table: "CredentialRequirement",
                column: "Cr3ExsKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirement_CEPeriod_Cr3Ce2Key",
                table: "CredentialRequirement",
                column: "Cr3Ce2Key",
                principalTable: "CEPeriod",
                principalColumn: "Ce2Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirement_Degrees_Cr3DegKey",
                table: "CredentialRequirement",
                column: "Cr3DegKey",
                principalTable: "Degrees",
                principalColumn: "DegKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirement_ExamScores_Cr3ExsKey",
                table: "CredentialRequirement",
                column: "Cr3ExsKey",
                principalTable: "ExamScores",
                principalColumn: "ExsKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirement_CEPeriod_Cr3Ce2Key",
                table: "CredentialRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirement_Degrees_Cr3DegKey",
                table: "CredentialRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirement_ExamScores_Cr3ExsKey",
                table: "CredentialRequirement");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirement_Cr3Ce2Key",
                table: "CredentialRequirement");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirement_Cr3DegKey",
                table: "CredentialRequirement");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirement_Cr3ExsKey",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "Cr3Ce2Key",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "Cr3DegKey",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "Cr3ExsKey",
                table: "CredentialRequirement");
        }
    }
}
