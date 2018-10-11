using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class makestatusstatecodenullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Ct6Cu6Key",
                table: "CredentialStatusType",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Ct6Cu6Key",
                table: "CredentialStatusType",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
