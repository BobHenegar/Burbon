using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedfieldstorequirementandrequirementtypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ct3EvalStoredProcedure",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Cr3AttestmentFlag",
                table: "CredentialRequirement",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr2AuditSelectDate",
                table: "CredentialProcess",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ct3EvalStoredProcedure",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Cr3AttestmentFlag",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "Cr2AuditSelectDate",
                table: "CredentialProcess");
        }
    }
}
