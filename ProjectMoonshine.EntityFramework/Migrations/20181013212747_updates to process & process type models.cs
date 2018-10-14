using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class updatestoprocessprocesstypemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ct1SyncFlag",
                table: "CredentialType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Ct2CalculateDueDateSp",
                table: "CredentialProcessType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct2CalculateStartDateSp",
                table: "CredentialProcessType",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Ct2DueDateNumber",
                table: "CredentialProcessType",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ct2DueDateRounding",
                table: "CredentialProcessType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ct2DueDateUnit",
                table: "CredentialProcessType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Ct2ProcessType",
                table: "CredentialProcessType",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr2DueDate",
                table: "CredentialProcess",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr2StartDate",
                table: "CredentialProcess",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr1ValidThruDate",
                table: "Credential",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ct1SyncFlag",
                table: "CredentialType");

            migrationBuilder.DropColumn(
                name: "Ct2CalculateDueDateSp",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Ct2CalculateStartDateSp",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Ct2DueDateNumber",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Ct2DueDateRounding",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Ct2DueDateUnit",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Ct2ProcessType",
                table: "CredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Cr2DueDate",
                table: "CredentialProcess");

            migrationBuilder.DropColumn(
                name: "Cr2StartDate",
                table: "CredentialProcess");

            migrationBuilder.DropColumn(
                name: "Cr1ValidThruDate",
                table: "Credential");
        }
    }
}
