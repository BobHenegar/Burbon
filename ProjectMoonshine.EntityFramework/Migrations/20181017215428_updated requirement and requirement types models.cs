using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class updatedrequirementandrequirementtypesmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ct3CalculateDueDateSp",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct3CalculateStartDateSp",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct3DependentCt6Key",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Ct3DueDateNumber",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ct3DueDateRounding",
                table: "CredentialRequirementType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ct3DueDateUnit",
                table: "CredentialRequirementType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Ct3HasRequiredParentsFlag",
                table: "CredentialRequirementType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct3StartCr2Ct6Key",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ct3SyncToProcessDatesFlag",
                table: "CredentialRequirementType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "Cr3Ct6Key",
                table: "CredentialRequirement",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3DependentCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3DependentCt6Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3StartCr2Ct6Key",
                table: "CredentialRequirementType",
                column: "Ct3StartCr2Ct6Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3DependentCt6Key",
                table: "CredentialRequirementType",
                column: "Ct3DependentCt6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3StartCr2Ct6Key",
                table: "CredentialRequirementType",
                column: "Ct3StartCr2Ct6Key",
                principalTable: "CredentialStatusType",
                principalColumn: "Ct6Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3DependentCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_CredentialStatusType_Ct3StartCr2Ct6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3DependentCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3StartCr2Ct6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3CalculateDueDateSp",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3CalculateStartDateSp",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3DependentCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3DueDateNumber",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3DueDateRounding",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3DueDateUnit",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3HasRequiredParentsFlag",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3StartCr2Ct6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3SyncToProcessDatesFlag",
                table: "CredentialRequirementType");

            migrationBuilder.AlterColumn<Guid>(
                name: "Cr3Ct6Key",
                table: "CredentialRequirement",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
