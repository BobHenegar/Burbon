using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class lowercasecr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cr1ValidThruDate",
                table: "Credentials",
                newName: "cr1ValidThruDate");

            migrationBuilder.RenameColumn(
                name: "Cr1StatusChangeUser",
                table: "Credentials",
                newName: "cr1StatusChangeUser");

            migrationBuilder.RenameColumn(
                name: "Cr1StatusChangeDate",
                table: "Credentials",
                newName: "cr1StatusChangeDate");

            migrationBuilder.RenameColumn(
                name: "Cr1Status",
                table: "Credentials",
                newName: "cr1Status");

            migrationBuilder.RenameColumn(
                name: "Cr1Owner",
                table: "Credentials",
                newName: "cr1Owner");

            migrationBuilder.RenameColumn(
                name: "Cr1Note",
                table: "Credentials",
                newName: "cr1Note");

            migrationBuilder.RenameColumn(
                name: "Cr1NextCr2Key",
                table: "Credentials",
                newName: "cr1NextCr2Key");

            migrationBuilder.RenameColumn(
                name: "Cr1LegalLastName",
                table: "Credentials",
                newName: "cr1LegalLastName");

            migrationBuilder.RenameColumn(
                name: "Cr1LegalFirstName",
                table: "Credentials",
                newName: "cr1LegalFirstName");

            migrationBuilder.RenameColumn(
                name: "Cr1ExtensionUsedFlag",
                table: "Credentials",
                newName: "cr1ExtensionUsedFlag");

            migrationBuilder.RenameColumn(
                name: "Cr1EffectiveDate",
                table: "Credentials",
                newName: "cr1EffectiveDate");

            migrationBuilder.RenameColumn(
                name: "Cr1DataKey",
                table: "Credentials",
                newName: "cr1DataKey");

            migrationBuilder.RenameColumn(
                name: "Cr1CurrentCr2Key",
                table: "Credentials",
                newName: "cr1CurrentCr2Key");

            migrationBuilder.RenameColumn(
                name: "Cr1Ct2CurrentProcess",
                table: "Credentials",
                newName: "cr1Ct2CurrentProcess");

            migrationBuilder.RenameColumn(
                name: "Cr1Ct1Credential",
                table: "Credentials",
                newName: "cr1Ct1Credential");

            migrationBuilder.RenameColumn(
                name: "Cr1CstKey",
                table: "Credentials",
                newName: "cr1CstKey");

            migrationBuilder.RenameColumn(
                name: "Cr1CredentialNumber",
                table: "Credentials",
                newName: "cr1CredentialNumber");

            migrationBuilder.RenameColumn(
                name: "Cr1CredentialDate",
                table: "Credentials",
                newName: "cr1CredentialDate");

            migrationBuilder.RenameColumn(
                name: "Cr1ChangeUser",
                table: "Credentials",
                newName: "cr1ChangeUser");

            migrationBuilder.RenameColumn(
                name: "Cr1ChangeDate",
                table: "Credentials",
                newName: "cr1ChangeDate");

            migrationBuilder.RenameColumn(
                name: "Cr1ApplicationCr2Key",
                table: "Credentials",
                newName: "cr1ApplicationCr2Key");

            migrationBuilder.RenameColumn(
                name: "Cr1AddUser",
                table: "Credentials",
                newName: "cr1AddUser");

            migrationBuilder.RenameColumn(
                name: "Cr1AddDate",
                table: "Credentials",
                newName: "cr1AddDate");

            migrationBuilder.RenameColumn(
                name: "Cr1Key",
                table: "Credentials",
                newName: "cr1Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cr1ValidThruDate",
                table: "Credentials",
                newName: "Cr1ValidThruDate");

            migrationBuilder.RenameColumn(
                name: "cr1StatusChangeUser",
                table: "Credentials",
                newName: "Cr1StatusChangeUser");

            migrationBuilder.RenameColumn(
                name: "cr1StatusChangeDate",
                table: "Credentials",
                newName: "Cr1StatusChangeDate");

            migrationBuilder.RenameColumn(
                name: "cr1Status",
                table: "Credentials",
                newName: "Cr1Status");

            migrationBuilder.RenameColumn(
                name: "cr1Owner",
                table: "Credentials",
                newName: "Cr1Owner");

            migrationBuilder.RenameColumn(
                name: "cr1Note",
                table: "Credentials",
                newName: "Cr1Note");

            migrationBuilder.RenameColumn(
                name: "cr1NextCr2Key",
                table: "Credentials",
                newName: "Cr1NextCr2Key");

            migrationBuilder.RenameColumn(
                name: "cr1LegalLastName",
                table: "Credentials",
                newName: "Cr1LegalLastName");

            migrationBuilder.RenameColumn(
                name: "cr1LegalFirstName",
                table: "Credentials",
                newName: "Cr1LegalFirstName");

            migrationBuilder.RenameColumn(
                name: "cr1ExtensionUsedFlag",
                table: "Credentials",
                newName: "Cr1ExtensionUsedFlag");

            migrationBuilder.RenameColumn(
                name: "cr1EffectiveDate",
                table: "Credentials",
                newName: "Cr1EffectiveDate");

            migrationBuilder.RenameColumn(
                name: "cr1DataKey",
                table: "Credentials",
                newName: "Cr1DataKey");

            migrationBuilder.RenameColumn(
                name: "cr1CurrentCr2Key",
                table: "Credentials",
                newName: "Cr1CurrentCr2Key");

            migrationBuilder.RenameColumn(
                name: "cr1Ct2CurrentProcess",
                table: "Credentials",
                newName: "Cr1Ct2CurrentProcess");

            migrationBuilder.RenameColumn(
                name: "cr1Ct1Credential",
                table: "Credentials",
                newName: "Cr1Ct1Credential");

            migrationBuilder.RenameColumn(
                name: "cr1CstKey",
                table: "Credentials",
                newName: "Cr1CstKey");

            migrationBuilder.RenameColumn(
                name: "cr1CredentialNumber",
                table: "Credentials",
                newName: "Cr1CredentialNumber");

            migrationBuilder.RenameColumn(
                name: "cr1CredentialDate",
                table: "Credentials",
                newName: "Cr1CredentialDate");

            migrationBuilder.RenameColumn(
                name: "cr1ChangeUser",
                table: "Credentials",
                newName: "Cr1ChangeUser");

            migrationBuilder.RenameColumn(
                name: "cr1ChangeDate",
                table: "Credentials",
                newName: "Cr1ChangeDate");

            migrationBuilder.RenameColumn(
                name: "cr1ApplicationCr2Key",
                table: "Credentials",
                newName: "Cr1ApplicationCr2Key");

            migrationBuilder.RenameColumn(
                name: "cr1AddUser",
                table: "Credentials",
                newName: "Cr1AddUser");

            migrationBuilder.RenameColumn(
                name: "cr1AddDate",
                table: "Credentials",
                newName: "Cr1AddDate");

            migrationBuilder.RenameColumn(
                name: "cr1Key",
                table: "Credentials",
                newName: "Cr1Key");
        }
    }
}
