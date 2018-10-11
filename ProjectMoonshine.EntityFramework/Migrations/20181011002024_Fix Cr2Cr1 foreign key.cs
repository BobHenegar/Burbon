using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class FixCr2Cr1foreignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcesses_Credentials_CredentialCr1Key",
                table: "CredentialProcesses");

            migrationBuilder.DropIndex(
                name: "IX_CredentialProcesses_CredentialCr1Key",
                table: "CredentialProcesses");

            migrationBuilder.DropColumn(
                name: "CredentialCr1Key",
                table: "CredentialProcesses");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcesses_Cr2Cr1Key",
                table: "CredentialProcesses",
                column: "Cr2Cr1Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcesses_Credentials_Cr2Cr1Key",
                table: "CredentialProcesses",
                column: "Cr2Cr1Key",
                principalTable: "Credentials",
                principalColumn: "Cr1Key",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcesses_Credentials_Cr2Cr1Key",
                table: "CredentialProcesses");

            migrationBuilder.DropIndex(
                name: "IX_CredentialProcesses_Cr2Cr1Key",
                table: "CredentialProcesses");

            migrationBuilder.AddColumn<Guid>(
                name: "CredentialCr1Key",
                table: "CredentialProcesses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcesses_CredentialCr1Key",
                table: "CredentialProcesses",
                column: "CredentialCr1Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcesses_Credentials_CredentialCr1Key",
                table: "CredentialProcesses",
                column: "CredentialCr1Key",
                principalTable: "Credentials",
                principalColumn: "Cr1Key",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
