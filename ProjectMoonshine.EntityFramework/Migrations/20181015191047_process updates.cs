using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class processupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ct6ParentCt6Key",
                table: "CredentialStatusType",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ct6SetCredentialNumberFlag",
                table: "CredentialStatusType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct2CompletedCr1Ct6Key",
                table: "CredentialProcessType",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Cr2Ct6Key",
                table: "CredentialProcess",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_CredentialStatusType_Ct6ParentCt6Key",
                table: "CredentialStatusType",
                column: "Ct6ParentCt6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcessType_Ct2CompletedCr1Ct6Key",
                table: "CredentialProcessType",
                column: "Ct2CompletedCr1Ct6Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcessType_CredentialStatusType_Ct2CompletedCr1Ct6Key",
                table: "CredentialProcessType",
                column: "Ct2CompletedCr1Ct6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialStatusType_CredentialStatusType_Ct6ParentCt6Key",
                table: "CredentialStatusType",
                column: "Ct6ParentCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcessType_CredentialStatusType_Ct2CompletedCr1Ct6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialStatusType_CredentialStatusType_Ct6ParentCt6Key",
                table: "CredentialStatusType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialStatusType_Ct6ParentCt6Key",
                table: "CredentialStatusType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialProcessType_Ct2CompletedCr1Ct6Key",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Ct6ParentCt6Key",
                table: "CredentialStatusType");

            migrationBuilder.DropColumn(
                name: "Ct6SetCredentialNumberFlag",
                table: "CredentialStatusType");

            migrationBuilder.DropColumn(
                name: "Ct2CompletedCr1Ct6Key",
                table: "CredentialProcessType");

            migrationBuilder.AlterColumn<Guid>(
                name: "Cr2Ct6Key",
                table: "CredentialProcess",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
