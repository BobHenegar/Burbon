using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedlicensemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Cr3IxlKey",
                table: "CredentialRequirement",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    IxlKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    IxlCusKey = table.Column<Guid>(nullable: false),
                    IxlState = table.Column<string>(nullable: true),
                    IxlLicenseNo = table.Column<string>(nullable: true),
                    IxlLicenseName = table.Column<string>(nullable: true),
                    IxlIssueDate = table.Column<DateTimeOffset>(nullable: false),
                    IxlExpireDate = table.Column<DateTimeOffset>(nullable: false),
                    IxlAddUser = table.Column<string>(nullable: true),
                    IxlAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    IxlChangeUser = table.Column<string>(nullable: true),
                    IxlChangeDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.IxlKey);
                    table.ForeignKey(
                        name: "FK_Licenses_Customer_IxlCusKey",
                        column: x => x.IxlCusKey,
                        principalTable: "Customer",
                        principalColumn: "CusKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirement_Cr3IxlKey",
                table: "CredentialRequirement",
                column: "Cr3IxlKey");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_IxlCusKey",
                table: "Licenses",
                column: "IxlCusKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirement_Licenses_Cr3IxlKey",
                table: "CredentialRequirement",
                column: "Cr3IxlKey",
                principalTable: "Licenses",
                principalColumn: "IxlKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirement_Licenses_Cr3IxlKey",
                table: "CredentialRequirement");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirement_Cr3IxlKey",
                table: "CredentialRequirement");

            migrationBuilder.DropColumn(
                name: "Cr3IxlKey",
                table: "CredentialRequirement");
        }
    }
}
