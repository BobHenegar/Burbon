using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class updatedcredentialmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ct2NextCt2key",
                table: "CredentialProcessType",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr2CompletionDate",
                table: "CredentialProcess",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr1CredentialDate",
                table: "Credential",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cr1CredentialNumber",
                table: "Credential",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr1EffectiveDate",
                table: "Credential",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcess_Cr2Ct2Key",
                table: "CredentialProcess",
                column: "Cr2Ct2Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcess_CredentialProcessType_Cr2Ct2Key",
                table: "CredentialProcess",
                column: "Cr2Ct2Key",
                principalTable: "CredentialProcessType",
                principalColumn: "Ct2Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcess_CredentialProcessType_Cr2Ct2Key",
                table: "CredentialProcess");

            migrationBuilder.DropIndex(
                name: "IX_CredentialProcess_Cr2Ct2Key",
                table: "CredentialProcess");

            migrationBuilder.DropColumn(
                name: "Ct2NextCt2key",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Cr2CompletionDate",
                table: "CredentialProcess");

            migrationBuilder.DropColumn(
                name: "Cr1CredentialDate",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "Cr1CredentialNumber",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "Cr1EffectiveDate",
                table: "Credential");
        }
    }
}
