using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addeddocumentanddegreemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FupDtpKey",
                table: "FileUploads",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct3DtpKey",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Cr3FupKey",
                table: "CredentialRequirement",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    DtpKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    DtpCode = table.Column<string>(nullable: false),
                    DtpDescription = table.Column<string>(nullable: true),
                    DtpOrder = table.Column<short>(nullable: true),
                    DtpAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    DtpChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    DtpAddUser = table.Column<string>(nullable: true),
                    DtpChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.DtpKey);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_FupDtpKey",
                table: "FileUploads",
                column: "FupDtpKey");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3DtpKey",
                table: "CredentialRequirementType",
                column: "Ct3DtpKey");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirement_Cr3FupKey",
                table: "CredentialRequirement",
                column: "Cr3FupKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirement_FileUploads_Cr3FupKey",
                table: "CredentialRequirement",
                column: "Cr3FupKey",
                principalTable: "FileUploads",
                principalColumn: "FupKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_DocumentType_Ct3DtpKey",
                table: "CredentialRequirementType",
                column: "Ct3DtpKey",
                principalTable: "DocumentType",
                principalColumn: "DtpKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploads_DocumentType_FupDtpKey",
                table: "FileUploads",
                column: "FupDtpKey",
                principalTable: "DocumentType",
                principalColumn: "DtpKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirement_FileUploads_Cr3FupKey",
                table: "CredentialRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_DocumentType_Ct3DtpKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_DocumentType_FupDtpKey",
                table: "FileUploads");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropIndex(
                name: "IX_FileUploads_FupDtpKey",
                table: "FileUploads");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3DtpKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirement_Cr3FupKey",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "FupDtpKey",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "Ct3DtpKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Cr3FupKey",
                table: "CredentialRequirement");
        }
    }
}
