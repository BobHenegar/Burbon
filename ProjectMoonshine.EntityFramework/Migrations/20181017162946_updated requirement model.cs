using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class updatedrequirementmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr3DueDate",
                table: "CredentialRequirement",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr3MetDate",
                table: "CredentialRequirement",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr3StartDate",
                table: "CredentialRequirement",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cr3DueDate",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "Cr3MetDate",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "Cr3StartDate",
                table: "CredentialRequirement");
        }
    }
}
