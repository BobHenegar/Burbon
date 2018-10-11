using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddedCredentialRequirementModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CredentialRequirements",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Cr3Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Cr3Ct3Key = table.Column<Guid>(nullable: false),
                    Cr3Cr2Key = table.Column<Guid>(nullable: false),
                    Cr3DataKey = table.Column<Guid>(nullable: true),
                    Cr3AddUser = table.Column<string>(nullable: true),
                    Cr3AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Cr3ChangeUser = table.Column<string>(nullable: true),
                    Cr3ChangeDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialRequirements", x => x.Cr3Key);
                    table.ForeignKey(
                        name: "FK_CredentialRequirements_CredentialProcesses_Cr3Cr2Key",
                        column: x => x.Cr3Cr2Key,
                        principalTable: "CredentialProcesses",
                        principalColumn: "Cr2Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CredentialRequirements_CredentialRequirementType_Cr3Ct3Key",
                        column: x => x.Cr3Ct3Key,
                        principalTable: "CredentialRequirementType",
                        principalColumn: "Ct3Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirements_Cr3Cr2Key",
                table: "CredentialRequirements",
                column: "Cr3Cr2Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirements_Cr3Ct3Key",
                table: "CredentialRequirements",
                column: "Cr3Ct3Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CredentialRequirements");
        }
    }
}
