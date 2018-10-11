using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class Addstatusstatemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ct6Cu6Key",
                table: "CredentialStatusType",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StatusStateType",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Cu6Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Cu6Code = table.Column<string>(nullable: true),
                    Cu6AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Cu6ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Cu6AddUser = table.Column<string>(nullable: true),
                    Cu6ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusStateType", x => x.Cu6Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CredentialStatusType_Ct6Cu6Key",
                table: "CredentialStatusType",
                column: "Ct6Cu6Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialStatusType_StatusStateType_Ct6Cu6Key",
                table: "CredentialStatusType",
                column: "Ct6Cu6Key",
                principalTable: "StatusStateType",
                principalColumn: "Cu6Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialStatusType_StatusStateType_Ct6Cu6Key",
                table: "CredentialStatusType");

            migrationBuilder.DropTable(
                name: "StatusStateType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialStatusType_Ct6Cu6Key",
                table: "CredentialStatusType");

            migrationBuilder.DropColumn(
                name: "Ct6Cu6Key",
                table: "CredentialStatusType");
        }
    }
}
