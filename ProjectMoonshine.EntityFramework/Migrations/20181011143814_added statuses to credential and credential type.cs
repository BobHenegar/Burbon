using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedstatusestocredentialandcredentialtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ct1InitCt6Key",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct1RenewCt6Key",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Cr1Ct6Key",
                table: "Credential",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CredentialType_Ct1InitCt6Key",
                table: "CredentialType",
                column: "Ct1InitCt6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialType_Ct1RenewCt6Key",
                table: "CredentialType",
                column: "Ct1RenewCt6Key");

            migrationBuilder.CreateIndex(
                name: "IX_Credential_Cr1Ct6Key",
                table: "Credential",
                column: "Cr1Ct6Key");

            migrationBuilder.AddForeignKey(
                name: "FK_Credential_CredentialStatusType_Cr1Ct6Key",
                table: "Credential",
                column: "Cr1Ct6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialType_CredentialStatusType_Ct1InitCt6Key",
                table: "CredentialType",
                column: "Ct1InitCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialType_CredentialStatusType_Ct1RenewCt6Key",
                table: "CredentialType",
                column: "Ct1RenewCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credential_CredentialStatusType_Cr1Ct6Key",
                table: "Credential");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialType_CredentialStatusType_Ct1InitCt6Key",
                table: "CredentialType");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialType_CredentialStatusType_Ct1RenewCt6Key",
                table: "CredentialType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialType_Ct1InitCt6Key",
                table: "CredentialType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialType_Ct1RenewCt6Key",
                table: "CredentialType");

            migrationBuilder.DropIndex(
                name: "IX_Credential_Cr1Ct6Key",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "Ct1InitCt6Key",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1RenewCt6Key",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Cr1Ct6Key",
                table: "Credential");
        }
    }
}
