using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class madecetypekeyonceperiodnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Ce2CetKey",
                table: "CEPeriod",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Ce2CetKey",
                table: "CEPeriod",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
