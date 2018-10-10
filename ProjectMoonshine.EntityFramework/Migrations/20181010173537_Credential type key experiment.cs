using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class Credentialtypekeyexperiment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cr1Ct1Credential",
                table: "Credentials");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Cr1AddDate",
                table: "Credentials",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "Cr1Key",
                table: "Credentials",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "Cr1Ct1Key",
                table: "Credentials",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_Cr1Ct1Key",
                table: "Credentials",
                column: "Cr1Ct1Key");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_CredentialType_Cr1Ct1Key",
                table: "Credentials",
                column: "Cr1Ct1Key",
                principalTable: "CredentialType",
                principalColumn: "Ct1Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_CredentialType_Cr1Ct1Key",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_Cr1Ct1Key",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1Ct1Key",
                table: "Credentials");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Cr1AddDate",
                table: "Credentials",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Cr1Key",
                table: "Credentials",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<string>(
                name: "Cr1Ct1Credential",
                table: "Credentials",
                nullable: true);
        }
    }
}
