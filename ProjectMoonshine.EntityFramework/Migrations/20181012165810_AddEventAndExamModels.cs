using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddEventAndExamModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_PerCusKey",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EvtKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    EvtDescription = table.Column<string>(maxLength: 500, nullable: false),
                    EvtName = table.Column<string>(maxLength: 500, nullable: false),
                    EvtStartDate = table.Column<DateTimeOffset>(nullable: true),
                    EvtEndDate = table.Column<DateTimeOffset>(nullable: true),
                    EvtAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    EvtChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    EvtAddUser = table.Column<string>(nullable: true),
                    EvtChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EvtKey);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    ExmKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    ExmEvtKey = table.Column<Guid>(nullable: true),
                    ExmSeatCount = table.Column<int>(nullable: true),
                    ExmCanRetake = table.Column<bool>(nullable: false),
                    ExmAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    ExmChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ExmAddUser = table.Column<string>(nullable: true),
                    ExmChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.ExmKey);
                    table.ForeignKey(
                        name: "FK_Exams_Events_ExmEvtKey",
                        column: x => x.ExmEvtKey,
                        principalTable: "Events",
                        principalColumn: "EvtKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventRegistrations",
                columns: table => new
                {
                    EvrKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    EvrEvtKey = table.Column<Guid>(nullable: false),
                    EvrCusKey = table.Column<Guid>(nullable: false),
                    EvrDropDate = table.Column<DateTimeOffset>(nullable: true),
                    EvrAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    EvrChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    EvrAddUser = table.Column<string>(nullable: true),
                    EvrChangeUser = table.Column<string>(nullable: true),
                    ExamExmKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistrations", x => x.EvrKey);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Customer_EvrCusKey",
                        column: x => x.EvrCusKey,
                        principalTable: "Customer",
                        principalColumn: "CusKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Events_EvrEvtKey",
                        column: x => x.EvrEvtKey,
                        principalTable: "Events",
                        principalColumn: "EvtKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Exams_ExamExmKey",
                        column: x => x.ExamExmKey,
                        principalTable: "Exams",
                        principalColumn: "ExmKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamScores",
                columns: table => new
                {
                    ExsKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    ExsExmKey = table.Column<Guid>(nullable: false),
                    ExsCusKey = table.Column<Guid>(nullable: false),
                    ExsTotalPossible = table.Column<int>(nullable: false),
                    ExsTotalReceived = table.Column<int>(nullable: false),
                    ExsAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    ExsChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    ExsAddUser = table.Column<string>(nullable: true),
                    ExsChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamScores", x => x.ExsKey);
                    table.ForeignKey(
                        name: "FK_ExamScores_Customer_ExsCusKey",
                        column: x => x.ExsCusKey,
                        principalTable: "Customer",
                        principalColumn: "CusKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamScores_Exams_ExsExmKey",
                        column: x => x.ExsExmKey,
                        principalTable: "Exams",
                        principalColumn: "ExmKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_PerCusKey",
                table: "Person",
                column: "PerCusKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_EvrCusKey",
                table: "EventRegistrations",
                column: "EvrCusKey");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_EvrEvtKey",
                table: "EventRegistrations",
                column: "EvrEvtKey");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_ExamExmKey",
                table: "EventRegistrations",
                column: "ExamExmKey");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExmEvtKey",
                table: "Exams",
                column: "ExmEvtKey");

            migrationBuilder.CreateIndex(
                name: "IX_ExamScores_ExsCusKey",
                table: "ExamScores",
                column: "ExsCusKey");

            migrationBuilder.CreateIndex(
                name: "IX_ExamScores_ExsExmKey",
                table: "ExamScores",
                column: "ExsExmKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRegistrations");

            migrationBuilder.DropTable(
                name: "ExamScores");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Person_PerCusKey",
                table: "Person");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PerCusKey",
                table: "Person",
                column: "PerCusKey");
        }
    }
}
