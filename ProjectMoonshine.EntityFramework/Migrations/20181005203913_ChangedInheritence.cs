using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class ChangedInheritence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Customer_CusPerKey",
                table: "Person");

            migrationBuilder.AlterColumn<Guid>(
                name: "CusPerKey",
                table: "Person",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Customer_CusPerKey",
                table: "Person",
                column: "CusPerKey",
                principalTable: "Customer",
                principalColumn: "CusKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Customer_CusPerKey",
                table: "Person");

            migrationBuilder.AlterColumn<Guid>(
                name: "CusPerKey",
                table: "Person",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Customer_CusPerKey",
                table: "Person",
                column: "CusPerKey",
                principalTable: "Customer",
                principalColumn: "CusKey",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
