using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class crossrecordmodelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Ct4EndDate",
                table: "CredentialTypeXCredentialProcessType",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Ct4StartDate",
                table: "CredentialTypeXCredentialProcessType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ct4EndDate",
                table: "CredentialTypeXCredentialProcessType");

            migrationBuilder.DropColumn(
                name: "Ct4StartDate",
                table: "CredentialTypeXCredentialProcessType");
        }
    }
}
