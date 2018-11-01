using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class flippedrelationshipbetweeneventsandexams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Events_ExmEvtKey",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_ExmEvtKey",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "ExmEvtKey",
                table: "Exams");

            migrationBuilder.AddColumn<Guid>(
                name: "EvtExmKey",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_EvtExmKey",
                table: "Events",
                column: "EvtExmKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Exams_EvtExmKey",
                table: "Events",
                column: "EvtExmKey",
                principalTable: "Exams",
                principalColumn: "ExmKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Exams_EvtExmKey",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EvtExmKey",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EvtExmKey",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "ExmEvtKey",
                table: "Exams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExmEvtKey",
                table: "Exams",
                column: "ExmEvtKey",
                unique: true,
                filter: "[ExmEvtKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Events_ExmEvtKey",
                table: "Exams",
                column: "ExmEvtKey",
                principalTable: "Events",
                principalColumn: "EvtKey",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
