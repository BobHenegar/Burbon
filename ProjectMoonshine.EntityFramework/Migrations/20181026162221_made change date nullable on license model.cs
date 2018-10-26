using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class madechangedatenullableonlicensemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "IxlChangeDate",
                table: "Licenses",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "IxlChangeDate",
                table: "Licenses",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);
        }
    }
}
