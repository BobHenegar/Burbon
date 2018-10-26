using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class updatedceperiodcedetailandstatusstatemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ct8Code",
                table: "CredentialRequirementTemplate",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Ct8SetDataKeyFlag",
                table: "CredentialRequirementTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Ce2AcceptedUnits",
                table: "CEPeriod",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Ce2BalanceUnits",
                table: "CEPeriod",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<Guid>(
                name: "Ce2CetKey",
                table: "CEPeriod",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<float>(
                name: "Ce2ReportedUnits",
                table: "CEPeriod",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Ce2RequiredUnits",
                table: "CEPeriod",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "Ce1VerifiedFlag",
                table: "CEDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CEPeriod_Ce2CetKey",
                table: "CEPeriod",
                column: "Ce2CetKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CEPeriod_CEType_Ce2CetKey",
                table: "CEPeriod",
                column: "Ce2CetKey",
                principalTable: "CEType",
                principalColumn: "CetKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CEPeriod_CEType_Ce2CetKey",
                table: "CEPeriod");

            migrationBuilder.DropIndex(
                name: "IX_CEPeriod_Ce2CetKey",
                table: "CEPeriod");

            migrationBuilder.DropColumn(
                name: "Ct8Code",
                table: "CredentialRequirementTemplate");

            migrationBuilder.DropColumn(
                name: "Ct8SetDataKeyFlag",
                table: "CredentialRequirementTemplate");

            migrationBuilder.DropColumn(
                name: "Ce2AcceptedUnits",
                table: "CEPeriod");

            migrationBuilder.DropColumn(
                name: "Ce2BalanceUnits",
                table: "CEPeriod");

            migrationBuilder.DropColumn(
                name: "Ce2CetKey",
                table: "CEPeriod");

            migrationBuilder.DropColumn(
                name: "Ce2ReportedUnits",
                table: "CEPeriod");

            migrationBuilder.DropColumn(
                name: "Ce2RequiredUnits",
                table: "CEPeriod");

            migrationBuilder.DropColumn(
                name: "Ce1VerifiedFlag",
                table: "CEDetail");
        }
    }
}
