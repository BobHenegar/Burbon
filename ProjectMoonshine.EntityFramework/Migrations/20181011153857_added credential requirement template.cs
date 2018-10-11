using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedcredentialrequirementtemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ct3Ct8Key",
                table: "CredentialRequirementType",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CredentialRequirementTemplate",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Ct8Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Ct8Template = table.Column<string>(nullable: true),
                    Ct8Description = table.Column<string>(nullable: true),
                    Ct8AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Ct8ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Ct8AddUser = table.Column<string>(nullable: true),
                    Ct8ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialRequirementTemplate", x => x.Ct8Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3Ct8Key",
                table: "CredentialRequirementType",
                column: "Ct3Ct8Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_CredentialRequirementTemplate_Ct3Ct8Key",
                table: "CredentialRequirementType",
                column: "Ct3Ct8Key",
                principalTable: "CredentialRequirementTemplate",
                principalColumn: "Ct8Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_CredentialRequirementTemplate_Ct3Ct8Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropTable(
                name: "CredentialRequirementTemplate");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3Ct8Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3Ct8Key",
                table: "CredentialRequirementType");
        }
    }
}
