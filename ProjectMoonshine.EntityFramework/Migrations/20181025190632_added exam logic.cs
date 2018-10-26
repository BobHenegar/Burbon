using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedexamlogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExsDate",
                table: "ExamScores",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "ExsSuccessFlag",
                table: "ExamScores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct3DeniedCt6Key",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Ct3ExamMaxAttempts",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Ct3ExmKey",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Ct3MinExamScore",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Ct3PriorExamScoreEligibleDateNumber",
                table: "CredentialRequirementType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ct3PriorExamScoreEligibleDateUnit",
                table: "CredentialRequirementType",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Ct3PriorExamScoreEligibleFlag",
                table: "CredentialRequirementType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirementType_Ct3ExmKey",
                table: "CredentialRequirementType",
                column: "Ct3ExmKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialRequirementType_Exams_Ct3ExmKey",
                table: "CredentialRequirementType",
                column: "Ct3ExmKey",
                principalTable: "Exams",
                principalColumn: "ExmKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialRequirementType_Exams_Ct3ExmKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialRequirementType_Ct3ExmKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "ExsDate",
                table: "ExamScores");

            migrationBuilder.DropColumn(
                name: "ExsSuccessFlag",
                table: "ExamScores");

            migrationBuilder.DropColumn(
                name: "Ct3DeniedCt6Key",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3ExamMaxAttempts",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3ExmKey",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3MinExamScore",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3PriorExamScoreEligibleDateNumber",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3PriorExamScoreEligibleDateUnit",
                table: "CredentialRequirementType");

            migrationBuilder.DropColumn(
                name: "Ct3PriorExamScoreEligibleFlag",
                table: "CredentialRequirementType");
        }
    }
}
