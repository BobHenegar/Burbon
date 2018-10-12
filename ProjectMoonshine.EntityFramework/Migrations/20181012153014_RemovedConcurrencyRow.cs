using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class RemovedConcurrencyRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "StatusStateType");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Phone");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CredentialTypeXCredentialProcessType");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CredentialStatusType");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CredentialRequirementTemplate");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CredentialProcessTypeXCredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CredentialProcess");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CEPeriod");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CEDetail");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "StatusStateType",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Phone",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Person",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Email",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Customer",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CredentialTypeXCredentialProcessType",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CredentialType",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CredentialStatusType",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CredentialRequirementType",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CredentialRequirementTemplate",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CredentialRequirement",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CredentialProcessTypeXCredentialRequirementType",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CredentialProcessType",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CredentialProcess",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Credential",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CEPeriod",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CEDetail",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Address",
                rowVersion: true,
                nullable: true);
        }
    }
}
