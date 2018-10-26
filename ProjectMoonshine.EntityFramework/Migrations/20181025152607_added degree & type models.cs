using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addeddegreetypemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ct3DetKey",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DegreeTypes",
                columns: table => new
                {
                    DetKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    DetCode = table.Column<string>(nullable: false),
                    DetDescription = table.Column<string>(nullable: true),
                    DetOrder = table.Column<short>(nullable: true),
                    DetAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    DetChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    DetAddUser = table.Column<string>(nullable: true),
                    DetChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreeTypes", x => x.DetKey);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    DegKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    DegCusKey = table.Column<Guid>(nullable: false),
                    DegInstitution = table.Column<string>(nullable: true),
                    DegDate = table.Column<DateTimeOffset>(nullable: false),
                    DegAddUser = table.Column<string>(nullable: true),
                    DegAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    DegChangeUser = table.Column<string>(nullable: true),
                    DegChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    DegDetKey = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.DegKey);
                    table.ForeignKey(
                        name: "FK_Degrees_Customer_DegCusKey",
                        column: x => x.DegCusKey,
                        principalTable: "Customer",
                        principalColumn: "CusKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Degrees_DegreeTypes_DegDetKey",
                        column: x => x.DegDetKey,
                        principalTable: "DegreeTypes",
                        principalColumn: "DetKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3DetKey",
                table: "CredentialRequirementType",
                column: "Ct3DetKey");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_DegCusKey",
                table: "Degrees",
                column: "DegCusKey");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_DegDetKey",
                table: "Degrees",
                column: "DegDetKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_DegreeTypes_Ct3DetKey",
                table: "CredentialRequirementType",
                column: "Ct3DetKey",
                principalTable: "DegreeTypes",
                principalColumn: "DetKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_DegreeTypes_Ct3DetKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "DegreeTypes");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3DetKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3DetKey",
                table: "CredentialRequirementType");
        }
    }
}
