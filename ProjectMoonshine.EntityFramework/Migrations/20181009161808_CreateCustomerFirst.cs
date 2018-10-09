using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class CreateCustomerFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From Person where CusPerKey is NULL");
            migrationBuilder.AlterColumn<Guid>(
                name: "CusPerKey",
                table: "Person",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CusPerKey",
                table: "Person",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
