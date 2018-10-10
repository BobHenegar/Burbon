using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class skinniedcredentialtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ct1AddDynKey",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1ClassAssembly",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1ClassName",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1CstType",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1DataTableName",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1DataTablePrefix",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1Designation",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1DesignationHtml",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1EditDynKey",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1InitCt2Class",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1InitCt2Key",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1InitCt2Method",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1InitStatus",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1LongDescription",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1OrgCstKey",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1ReinstateCt2Class",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1ReinstateCt2Key",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1ReinstateCt2Method",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1ReinstateStatus",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1RenewCt2Class",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1RenewCt2Method",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1RenewStatus",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1SetCredentialNumberSp",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1SyncCt1Key",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct1SyncFlag",
                table: "CredentialType");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Ct1StartDate",
                table: "CredentialType",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<int>(
                name: "Ct1Order",
                table: "CredentialType",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Ct1EndDate",
                table: "CredentialType",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Ct1StartDate",
                table: "CredentialType",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ct1Order",
                table: "CredentialType",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Ct1EndDate",
                table: "CredentialType",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct1AddDynKey",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1ClassAssembly",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1ClassName",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1CstType",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1DataTableName",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1DataTablePrefix",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1Designation",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1DesignationHtml",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct1EditDynKey",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1InitCt2Class",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct1InitCt2Key",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1InitCt2Method",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1InitStatus",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1LongDescription",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct1OrgCstKey",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1ReinstateCt2Class",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct1ReinstateCt2Key",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1ReinstateCt2Method",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1ReinstateStatus",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1RenewCt2Class",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1RenewCt2Method",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1RenewStatus",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1SetCredentialNumberSp",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct1SyncCt1Key",
                table: "CredentialType",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ct1SyncFlag",
                table: "CredentialType",
                nullable: false,
                defaultValue: false);
        }
    }
}
