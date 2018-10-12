using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class FixedEventRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Exams_ExamExmKey",
                table: "EventRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_ExamExmKey",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "ExamExmKey",
                table: "EventRegistrations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExamExmKey",
                table: "EventRegistrations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_ExamExmKey",
                table: "EventRegistrations",
                column: "ExamExmKey");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Exams_ExamExmKey",
                table: "EventRegistrations",
                column: "ExamExmKey",
                principalTable: "Exams",
                principalColumn: "ExmKey",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
