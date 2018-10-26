using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class remnants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_DocumentType_Ct3DtpKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_DocumentType_FupDtpKey",
                table: "FileUploads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentType",
                table: "DocumentType");

            migrationBuilder.RenameTable(
                name: "DocumentType",
                newName: "DocumentTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes",
                column: "DtpKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_DocumentTypes_Ct3DtpKey",
                table: "CredentialRequirementType",
                column: "Ct3DtpKey",
                principalTable: "DocumentTypes",
                principalColumn: "DtpKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploads_DocumentTypes_FupDtpKey",
                table: "FileUploads",
                column: "FupDtpKey",
                principalTable: "DocumentTypes",
                principalColumn: "DtpKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_DocumentTypes_Ct3DtpKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_DocumentTypes_FupDtpKey",
                table: "FileUploads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                newName: "DocumentType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentType",
                table: "DocumentType",
                column: "DtpKey");

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
    }
}
