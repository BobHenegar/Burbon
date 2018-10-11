using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddCredentialStatusTypemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcesses_Credentials_Cr2Cr1Key",
                table: "CredentialProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirements_CredentialProcesses_Cr3Cr2Key",
                table: "CredentialRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirements_CredentialRequirementType_Cr3Ct3Key",
                table: "CredentialRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_CredentialType_Cr1Ct1Key",
                table: "Credentials");

            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_CredentialProcesses_Cr1CurrentCr2Key",
                table: "Credentials");

            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Customer_Cr1CusKey",
                table: "Credentials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Credentials",
                table: "Credentials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CredentialRequirements",
                table: "CredentialRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CredentialProcesses",
                table: "CredentialProcesses");

            migrationBuilder.RenameTable(
                name: "Credentials",
                newName: "Credential");

            migrationBuilder.RenameTable(
                name: "CredentialRequirements",
                newName: "CredentialRequirement");

            migrationBuilder.RenameTable(
                name: "CredentialProcesses",
                newName: "CredentialProcess");

            migrationBuilder.RenameIndex(
                name: "IX_Credentials_Cr1CusKey",
                table: "Credential",
                newName: "IX_Credential_Cr1CusKey");

            migrationBuilder.RenameIndex(
                name: "IX_Credentials_Cr1CurrentCr2Key",
                table: "Credential",
                newName: "IX_Credential_Cr1CurrentCr2Key");

            migrationBuilder.RenameIndex(
                name: "IX_Credentials_Cr1Ct1Key",
                table: "Credential",
                newName: "IX_Credential_Cr1Ct1Key");

            migrationBuilder.RenameIndex(
                name: "IX_CredentialRequirements_Cr3Ct3Key",
                table: "CredentialRequirement",
                newName: "IX_CredentialRequirement_Cr3Ct3Key");

            migrationBuilder.RenameIndex(
                name: "IX_CredentialRequirements_Cr3Cr2Key",
                table: "CredentialRequirement",
                newName: "IX_CredentialRequirement_Cr3Cr2Key");

            migrationBuilder.RenameIndex(
                name: "IX_CredentialProcesses_Cr2Cr1Key",
                table: "CredentialProcess",
                newName: "IX_CredentialProcess_Cr2Cr1Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Credential",
                table: "Credential",
                column: "Cr1Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CredentialRequirement",
                table: "CredentialRequirement",
                column: "Cr3Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CredentialProcess",
                table: "CredentialProcess",
                column: "Cr2Key");

            migrationBuilder.CreateTable(
                name: "CredentialStatusType",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Ct6Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Ct6Code = table.Column<string>(nullable: true),
                    Ct6Description = table.Column<string>(nullable: true),
                    Ct6AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Ct6ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Ct6AddUser = table.Column<string>(nullable: true),
                    Ct6ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialStatusType", x => x.Ct6Key);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Credential_CredentialType_Cr1Ct1Key",
                table: "Credential",
                column: "Cr1Ct1Key",
                principalTable: "CredentialType",
                principalColumn: "Ct1Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Credential_CredentialProcess_Cr1CurrentCr2Key",
                table: "Credential",
                column: "Cr1CurrentCr2Key",
                principalTable: "CredentialProcess",
                principalColumn: "Cr2Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Credential_Customer_Cr1CusKey",
                table: "Credential",
                column: "Cr1CusKey",
                principalTable: "Customer",
                principalColumn: "CusKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcess_Credential_Cr2Cr1Key",
                table: "CredentialProcess",
                column: "Cr2Cr1Key",
                principalTable: "Credential",
                principalColumn: "Cr1Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirement_CredentialProcess_Cr3Cr2Key",
                table: "CredentialRequirement",
                column: "Cr3Cr2Key",
                principalTable: "CredentialProcess",
                principalColumn: "Cr2Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirement_CredentialRequirementType_Cr3Ct3Key",
                table: "CredentialRequirement",
                column: "Cr3Ct3Key",
                principalTable: "CredentialRequirementType",
                principalColumn: "Ct3Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credential_CredentialType_Cr1Ct1Key",
                table: "Credential");

            migrationBuilder.DropForeignKey(
                name: "FK_Credential_CredentialProcess_Cr1CurrentCr2Key",
                table: "Credential");

            migrationBuilder.DropForeignKey(
                name: "FK_Credential_Customer_Cr1CusKey",
                table: "Credential");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcess_Credential_Cr2Cr1Key",
                table: "CredentialProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirement_CredentialProcess_Cr3Cr2Key",
                table: "CredentialRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirement_CredentialRequirementType_Cr3Ct3Key",
                table: "CredentialRequirement");

            migrationBuilder.DropTable(
                name: "CredentialStatusType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CredentialRequirement",
                table: "CredentialRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CredentialProcess",
                table: "CredentialProcess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Credential",
                table: "Credential");

            migrationBuilder.RenameTable(
                name: "CredentialRequirement",
                newName: "CredentialRequirements");

            migrationBuilder.RenameTable(
                name: "CredentialProcess",
                newName: "CredentialProcesses");

            migrationBuilder.RenameTable(
                name: "Credential",
                newName: "Credentials");

            migrationBuilder.RenameIndex(
                name: "IX_CredentialRequirement_Cr3Ct3Key",
                table: "CredentialRequirements",
                newName: "IX_CredentialRequirements_Cr3Ct3Key");

            migrationBuilder.RenameIndex(
                name: "IX_CredentialRequirement_Cr3Cr2Key",
                table: "CredentialRequirements",
                newName: "IX_CredentialRequirements_Cr3Cr2Key");

            migrationBuilder.RenameIndex(
                name: "IX_CredentialProcess_Cr2Cr1Key",
                table: "CredentialProcesses",
                newName: "IX_CredentialProcesses_Cr2Cr1Key");

            migrationBuilder.RenameIndex(
                name: "IX_Credential_Cr1CusKey",
                table: "Credentials",
                newName: "IX_Credentials_Cr1CusKey");

            migrationBuilder.RenameIndex(
                name: "IX_Credential_Cr1CurrentCr2Key",
                table: "Credentials",
                newName: "IX_Credentials_Cr1CurrentCr2Key");

            migrationBuilder.RenameIndex(
                name: "IX_Credential_Cr1Ct1Key",
                table: "Credentials",
                newName: "IX_Credentials_Cr1Ct1Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CredentialRequirements",
                table: "CredentialRequirements",
                column: "Cr3Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CredentialProcesses",
                table: "CredentialProcesses",
                column: "Cr2Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Credentials",
                table: "Credentials",
                column: "Cr1Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcesses_Credentials_Cr2Cr1Key",
                table: "CredentialProcesses",
                column: "Cr2Cr1Key",
                principalTable: "Credentials",
                principalColumn: "Cr1Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirements_CredentialProcesses_Cr3Cr2Key",
                table: "CredentialRequirements",
                column: "Cr3Cr2Key",
                principalTable: "CredentialProcesses",
                principalColumn: "Cr2Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirements_CredentialRequirementType_Cr3Ct3Key",
                table: "CredentialRequirements",
                column: "Cr3Ct3Key",
                principalTable: "CredentialRequirementType",
                principalColumn: "Ct3Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_CredentialType_Cr1Ct1Key",
                table: "Credentials",
                column: "Cr1Ct1Key",
                principalTable: "CredentialType",
                principalColumn: "Ct1Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_CredentialProcesses_Cr1CurrentCr2Key",
                table: "Credentials",
                column: "Cr1CurrentCr2Key",
                principalTable: "CredentialProcesses",
                principalColumn: "Cr2Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Customer_Cr1CusKey",
                table: "Credentials",
                column: "Cr1CusKey",
                principalTable: "Customer",
                principalColumn: "CusKey",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
