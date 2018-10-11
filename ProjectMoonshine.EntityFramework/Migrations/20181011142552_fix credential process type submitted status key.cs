using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class fixcredentialprocesstypesubmittedstatuskey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ct6SubmittedCt6Key",
                table: "CredentialProcessType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ct6SubmittedCt6Key",
                table: "CredentialProcessType",
                nullable: true);
        }
    }
}
