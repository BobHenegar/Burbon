using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddCredentialTypeXCredentialProcessType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CredentialTypeXCredentialProcessType",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Ct4Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Ct4Ct1Key = table.Column<Guid>(nullable: false),
                    Ct4Ct2Key = table.Column<Guid>(nullable: false),
                    Ct4Order = table.Column<int>(nullable: false),
                    Ct4AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Ct4ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Ct4AddUser = table.Column<string>(nullable: true),
                    Ct4ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialTypeXCredentialProcessType", x => x.Ct4Key);
                    table.ForeignKey(
                        name: "FK_CredentialTypeXCredentialProcessType_CredentialType_Ct4Ct1Key",
                        column: x => x.Ct4Ct1Key,
                        principalTable: "CredentialType",
                        principalColumn: "Ct1Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CredentialTypeXCredentialProcessType_CredentialProcessType_Ct4Ct2Key",
                        column: x => x.Ct4Ct2Key,
                        principalTable: "CredentialProcessType",
                        principalColumn: "Ct2Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CredentialTypeXCredentialProcessType_Ct4Ct1Key",
                table: "CredentialTypeXCredentialProcessType",
                column: "Ct4Ct1Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialTypeXCredentialProcessType_Ct4Ct2Key",
                table: "CredentialTypeXCredentialProcessType",
                column: "Ct4Ct2Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CredentialTypeXCredentialProcessType");
        }
    }
}
