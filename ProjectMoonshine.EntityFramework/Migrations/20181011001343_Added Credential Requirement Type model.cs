using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddedCredentialRequirementTypemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CredentialRequirementType",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Ct3Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Ct3Requirement = table.Column<string>(nullable: true),
                    Ct3Description = table.Column<string>(nullable: true),
                    Ct3LongDescription = table.Column<string>(nullable: true),
                    Ct3AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Ct3ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Ct3AddUser = table.Column<string>(nullable: true),
                    Ct3ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialRequirementType", x => x.Ct3Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CredentialRequirementType");
        }
    }
}
