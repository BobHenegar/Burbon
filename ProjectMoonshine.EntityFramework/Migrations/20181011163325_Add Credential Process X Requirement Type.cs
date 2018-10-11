using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddCredentialProcessXRequirementType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CredentialProcessTypeXCredentialRequirementType",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Ct5Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Ct5Ct2Key = table.Column<Guid>(nullable: false),
                    Ct5Ct3Key = table.Column<Guid>(nullable: false),
                    Ct5Order = table.Column<int>(nullable: false),
                    Ct5SubmitPrereqFlag = table.Column<bool>(nullable: false),
                    Ct5ExamPrereqFlag = table.Column<bool>(nullable: false),
                    Ct5ReviewPrereqFlag = table.Column<bool>(nullable: false),
                    Ct5AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Ct5ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Ct5AddUser = table.Column<string>(nullable: true),
                    Ct5ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialProcessTypeXCredentialRequirementType", x => x.Ct5Key);
                    table.ForeignKey(
                        name: "FK_CredentialProcessTypeXCredentialRequirementType_CredentialProcessType_Ct5Ct2Key",
                        column: x => x.Ct5Ct2Key,
                        principalTable: "CredentialProcessType",
                        principalColumn: "Ct2Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CredentialProcessTypeXCredentialRequirementType_CredentialRequirementType_Ct5Ct3Key",
                        column: x => x.Ct5Ct3Key,
                        principalTable: "CredentialRequirementType",
                        principalColumn: "Ct3Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcessTypeXCredentialRequirementType_Ct5Ct2Key",
                table: "CredentialProcessTypeXCredentialRequirementType",
                column: "Ct5Ct2Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcessTypeXCredentialRequirementType_Ct5Ct3Key",
                table: "CredentialProcessTypeXCredentialRequirementType",
                column: "Ct5Ct3Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CredentialProcessTypeXCredentialRequirementType");
        }
    }
}
