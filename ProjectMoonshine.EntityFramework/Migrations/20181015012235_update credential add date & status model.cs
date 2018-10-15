using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class updatecredentialadddatestatusmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Cr1StartDate",
                table: "Credential",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "Cr1Ct6Key",
                table: "Credential",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Cr1StartDate",
                table: "Credential",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Cr1Ct6Key",
                table: "Credential",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
