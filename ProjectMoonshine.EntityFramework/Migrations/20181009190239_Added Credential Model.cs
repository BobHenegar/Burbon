using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddedCredentialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Cr1Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Cr1Ct1Credential = table.Column<string>(nullable: true),
                    Cr1CredentialNumber = table.Column<string>(nullable: true),
                    Cr1CstKey = table.Column<Guid>(nullable: false),
                    Cr1Status = table.Column<string>(nullable: true),
                    Cr1StatusChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    Cr1StatusChangeUser = table.Column<string>(nullable: true),
                    Cr1CredentialDate = table.Column<DateTimeOffset>(nullable: false),
                    Cr1EffectiveDate = table.Column<DateTimeOffset>(nullable: false),
                    Cr1ValidThruDate = table.Column<DateTimeOffset>(nullable: false),
                    Cr1Ct2CurrentProcess = table.Column<Guid>(nullable: false),
                    Cr1CurrentCr2Key = table.Column<Guid>(nullable: false),
                    Cr1NextCr2Key = table.Column<Guid>(nullable: false),
                    Cr1ApplicationCr2Key = table.Column<Guid>(nullable: false),
                    Cr1ExtensionUsedFlag = table.Column<bool>(nullable: false),
                    Cr1Note = table.Column<string>(nullable: true),
                    Cr1Owner = table.Column<string>(nullable: true),
                    Cr1DataKey = table.Column<Guid>(nullable: false),
                    Cr1AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Cr1ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Cr1AddUser = table.Column<string>(nullable: true),
                    Cr1ChangeUser = table.Column<string>(nullable: true),
                    Cr1LegalFirstName = table.Column<string>(nullable: true),
                    Cr1LegalLastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Cr1Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credentials");
        }
    }
}
