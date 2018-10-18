using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class Updatedstatemodelandsomeothermodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cu6TerminateProcessFlag",
                table: "StatusStateType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Ct5EndDate",
                table: "CredentialProcessTypeXCredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Ct5StartDate",
                table: "CredentialProcessTypeXCredentialRequirementType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cu6TerminateProcessFlag",
                table: "StatusStateType");

            migrationBuilder.DropColumn(
                name: "Ct5EndDate",
                table: "CredentialProcessTypeXCredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct5StartDate",
                table: "CredentialProcessTypeXCredentialRequirementType");
        }
    }
}
