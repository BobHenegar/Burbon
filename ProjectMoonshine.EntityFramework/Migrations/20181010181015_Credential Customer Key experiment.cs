using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class CredentialCustomerKeyexperiment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Person_PersonPerKey",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_PersonPerKey",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1CredentialNumber",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "PersonPerKey",
                table: "Credentials");

            migrationBuilder.RenameColumn(
                name: "Cr1PerKey",
                table: "Credentials",
                newName: "Cr1CusKey");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_Cr1CusKey",
                table: "Credentials",
                column: "Cr1CusKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Person_Cr1CusKey",
                table: "Credentials",
                column: "Cr1CusKey",
                principalTable: "Person",
                principalColumn: "PerKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Person_Cr1CusKey",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_Cr1CusKey",
                table: "Credentials");

            migrationBuilder.RenameColumn(
                name: "Cr1CusKey",
                table: "Credentials",
                newName: "Cr1PerKey");

            migrationBuilder.AddColumn<string>(
                name: "Cr1CredentialNumber",
                table: "Credentials",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonPerKey",
                table: "Credentials",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_PersonPerKey",
                table: "Credentials",
                column: "PersonPerKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Person_PersonPerKey",
                table: "Credentials",
                column: "PersonPerKey",
                principalTable: "Person",
                principalColumn: "PerKey",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
