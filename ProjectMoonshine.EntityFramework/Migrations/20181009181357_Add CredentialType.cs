using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddCredentialType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CredentialType",
                columns: table => new
                {
                    Ct1Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Ct1Credential = table.Column<string>(nullable: true),
                    Ct1Description = table.Column<string>(nullable: true),
                    Ct1LongDescription = table.Column<string>(nullable: true),
                    Ct1Designation = table.Column<string>(nullable: true),
                    Ct1DesignationHtml = table.Column<string>(nullable: true),
                    Ct1OrgCstKey = table.Column<Guid>(nullable: true),
                    Ct1StartDate = table.Column<DateTimeOffset>(nullable: false),
                    Ct1EndDate = table.Column<DateTimeOffset>(nullable: false),
                    Ct1InitCt2Key = table.Column<Guid>(nullable: true),
                    Ct1InitCt2Class = table.Column<string>(nullable: true),
                    Ct1InitCt2Method = table.Column<string>(nullable: true),
                    Ct1InitStatus = table.Column<string>(nullable: true),
                    Ct1RenewCt2Key = table.Column<Guid>(nullable: true),
                    Ct1RenewCt2Class = table.Column<string>(nullable: true),
                    Ct1RenewCt2Method = table.Column<string>(nullable: true),
                    Ct1RenewStatus = table.Column<string>(nullable: true),
                    Ct1ReinstateCt2Key = table.Column<Guid>(nullable: true),
                    Ct1ReinstateCt2Class = table.Column<string>(nullable: true),
                    Ct1ReinstateCt2Method = table.Column<string>(nullable: true),
                    Ct1ReinstateStatus = table.Column<string>(nullable: true),
                    Ct1DataTableName = table.Column<string>(nullable: true),
                    Ct1DataTablePrefix = table.Column<string>(nullable: true),
                    Ct1ClassAssembly = table.Column<string>(nullable: true),
                    Ct1ClassName = table.Column<string>(nullable: true),
                    Ct1AddDynKey = table.Column<Guid>(nullable: true),
                    Ct1EditDynKey = table.Column<Guid>(nullable: true),
                    Ct1CstType = table.Column<string>(nullable: true),
                    Ct1Order = table.Column<string>(nullable: true),
                    Ct1SyncFlag = table.Column<bool>(nullable: false),
                    Ct1SyncCt1Key = table.Column<string>(nullable: true),
                    Ct1SetCredentialNumberSp = table.Column<string>(nullable: true),
                    Ct1AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Ct1ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Ct1AddUser = table.Column<string>(nullable: true),
                    Ct1ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialType", x => x.Ct1Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CredentialType");
        }
    }
}
