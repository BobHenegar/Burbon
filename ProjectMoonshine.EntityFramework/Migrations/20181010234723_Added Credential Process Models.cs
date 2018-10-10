using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddedCredentialProcessModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Cr1CurrentCr2Key",
                table: "Credentials",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CredentialProcesses",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Cr2Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Cr2Ct2Key = table.Column<Guid>(nullable: false),
                    Cr2Cr1Key = table.Column<Guid>(nullable: false),
                    Cr2AddUser = table.Column<string>(nullable: true),
                    Cr2AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Cr2ChangeUser = table.Column<string>(nullable: true),
                    Cr2ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    CredentialCr1Key = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialProcesses", x => x.Cr2Key);
                    table.ForeignKey(
                        name: "FK_CredentialProcesses_Credentials_CredentialCr1Key",
                        column: x => x.CredentialCr1Key,
                        principalTable: "Credentials",
                        principalColumn: "Cr1Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CredentialProcessType",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Ct2Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Ct2Process = table.Column<string>(nullable: true),
                    Ct2Description = table.Column<string>(nullable: true),
                    Ct2AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Ct2ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Ct2AddUser = table.Column<string>(nullable: true),
                    Ct2ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialProcessType", x => x.Ct2Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_Cr1CurrentCr2Key",
                table: "Credentials",
                column: "Cr1CurrentCr2Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcesses_CredentialCr1Key",
                table: "CredentialProcesses",
                column: "CredentialCr1Key");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_CredentialProcesses_Cr1CurrentCr2Key",
                table: "Credentials",
                column: "Cr1CurrentCr2Key",
                principalTable: "CredentialProcesses",
                principalColumn: "Cr2Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_CredentialProcesses_Cr1CurrentCr2Key",
                table: "Credentials");

            migrationBuilder.DropTable(
                name: "CredentialProcesses");

            migrationBuilder.DropTable(
                name: "CredentialProcessType");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_Cr1CurrentCr2Key",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1CurrentCr2Key",
                table: "Credentials");
        }
    }
}
