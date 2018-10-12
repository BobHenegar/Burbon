using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class CredentialProcessmodelupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ct1InitCt2Key",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Cr1Ct2Key",
                table: "Credential",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr1StartDate",
                table: "Credential",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_CredentialType_Ct1InitCt2Key",
                table: "CredentialType",
                column: "Ct1InitCt2Key");

            migrationBuilder.CreateIndex(
                name: "IX_Credential_Cr1Ct2Key",
                table: "Credential",
                column: "Cr1Ct2Key");

            migrationBuilder.AddForeignKey(
                name: "FK_Credential_CredentialProcessType_Cr1Ct2Key",
                table: "Credential",
                column: "Cr1Ct2Key",
                principalTable: "CredentialProcessType",
                principalColumn: "Ct2Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialType_CredentialProcessType_Ct1InitCt2Key",
                table: "CredentialType",
                column: "Ct1InitCt2Key",
                principalTable: "CredentialProcessType",
                principalColumn: "Ct2Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credential_CredentialProcessType_Cr1Ct2Key",
                table: "Credential");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialType_CredentialProcessType_Ct1InitCt2Key",
                table: "CredentialType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialType_Ct1InitCt2Key",
                table: "CredentialType");

            migrationBuilder.DropIndex(
                name: "IX_Credential_Cr1Ct2Key",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "Ct1InitCt2Key",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Cr1Ct2Key",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "Cr1StartDate",
                table: "Credential");
        }
    }
}
