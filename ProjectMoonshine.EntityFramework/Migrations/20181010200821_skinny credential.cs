using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class skinnycredential : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CredentialRequirement");

            migrationBuilder.DropTable(
                name: "CredentialProcess");

            migrationBuilder.DropColumn(
                name: "Cr1ApplicationCr2Key",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1CredentialDate",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1Ct2CurrentProcess",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1CurrentCr2Key",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1DataKey",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1EffectiveDate",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1ExtensionUsedFlag",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1LegalFirstName",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1LegalLastName",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1NextCr2Key",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1Note",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1Owner",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1Status",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1StatusChangeDate",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1StatusChangeUser",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Cr1ValidThruDate",
                table: "Credentials");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Cr1ApplicationCr2Key",
                table: "Credentials",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr1CredentialDate",
                table: "Credentials",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "Cr1Ct2CurrentProcess",
                table: "Credentials",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Cr1CurrentCr2Key",
                table: "Credentials",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Cr1DataKey",
                table: "Credentials",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr1EffectiveDate",
                table: "Credentials",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "Cr1ExtensionUsedFlag",
                table: "Credentials",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Cr1LegalFirstName",
                table: "Credentials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cr1LegalLastName",
                table: "Credentials",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Cr1NextCr2Key",
                table: "Credentials",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Cr1Note",
                table: "Credentials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cr1Owner",
                table: "Credentials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cr1Status",
                table: "Credentials",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr1StatusChangeDate",
                table: "Credentials",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Cr1StatusChangeUser",
                table: "Credentials",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Cr1ValidThruDate",
                table: "Credentials",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "CredentialProcess",
                columns: table => new
                {
                    cr2Key = table.Column<Guid>(nullable: false),
                    CredentialCr1Key = table.Column<Guid>(nullable: true),
                    PersonPerKey = table.Column<Guid>(nullable: true),
                    cr2AddDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2AddUser = table.Column<string>(nullable: true),
                    cr2Agreement = table.Column<string>(nullable: true),
                    cr2AgreementDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2AuditCompleteDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2AuditCompleteUser = table.Column<DateTimeOffset>(nullable: false),
                    cr2AuditSelectDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2ChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2ChangeUser = table.Column<string>(nullable: true),
                    cr2CompletionDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2Cr1Key = table.Column<Guid>(nullable: false),
                    cr2Cr2Key = table.Column<Guid>(nullable: false),
                    cr2Ct2Process = table.Column<string>(nullable: true),
                    cr2DataKey = table.Column<Guid>(nullable: false),
                    cr2Due2Date = table.Column<DateTimeOffset>(nullable: false),
                    cr2DueDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2EthicsClearedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2EthicsDisclosure = table.Column<string>(nullable: true),
                    cr2EthicsSummary = table.Column<string>(nullable: true),
                    cr2Extension2Date = table.Column<DateTimeOffset>(nullable: false),
                    cr2Extension2GrantedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2Extension2GrantedReason = table.Column<string>(nullable: true),
                    cr2Extension2GrantedUser = table.Column<DateTimeOffset>(nullable: false),
                    cr2ExtensionDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2ExtensionGrantedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2ExtensionGrantedReason = table.Column<string>(nullable: true),
                    cr2ExtensionGrantedUser = table.Column<string>(nullable: true),
                    cr2FulfillmentDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2InactiveEndDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2InactiveStartDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2Note = table.Column<string>(nullable: true),
                    cr2Owner = table.Column<string>(nullable: true),
                    cr2PerKey = table.Column<Guid>(nullable: false),
                    cr2ProcessNumber = table.Column<string>(nullable: true),
                    cr2StartDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2Status = table.Column<string>(nullable: true),
                    cr2StatusChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2StatusChangeUser = table.Column<string>(nullable: true),
                    cr2SubmitDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2TerminationDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialProcess", x => x.cr2Key);
                    table.ForeignKey(
                        name: "FK_CredentialProcess_Credentials_CredentialCr1Key",
                        column: x => x.CredentialCr1Key,
                        principalTable: "Credentials",
                        principalColumn: "Cr1Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CredentialProcess_Person_PersonPerKey",
                        column: x => x.PersonPerKey,
                        principalTable: "Person",
                        principalColumn: "PerKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CredentialRequirement",
                columns: table => new
                {
                    cr3Key = table.Column<Guid>(nullable: false),
                    CredentialCr1Key = table.Column<Guid>(nullable: true),
                    CredentialProcesscr2Key = table.Column<Guid>(nullable: true),
                    PersonPerKey = table.Column<Guid>(nullable: true),
                    cr3AddDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3AddUser = table.Column<string>(nullable: true),
                    cr3AttestmentFlag = table.Column<bool>(nullable: false),
                    cr3CPerKey = table.Column<Guid>(nullable: false),
                    cr3ChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3ChangeUser = table.Column<string>(nullable: true),
                    cr3ChoiceCt3Key = table.Column<Guid>(nullable: false),
                    cr3Cr2Key = table.Column<Guid>(nullable: false),
                    cr3Cr3Key = table.Column<Guid>(nullable: false),
                    cr3Ct3Requirement = table.Column<string>(nullable: true),
                    cr3DataKey = table.Column<Guid>(nullable: false),
                    cr3DueDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3ExamAttempts = table.Column<short>(nullable: false),
                    cr3Extension2Date = table.Column<DateTimeOffset>(nullable: false),
                    cr3Extension2GrantedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3Extension2GrantedReason = table.Column<string>(nullable: true),
                    cr3Extension2GrantedUser = table.Column<string>(nullable: true),
                    cr3ExtensionDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3ExtensionGrantedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3ExtensionGrantedReason = table.Column<string>(nullable: true),
                    cr3ExtensionGrantedUser = table.Column<string>(nullable: true),
                    cr3HideOnlineFlag = table.Column<bool>(nullable: false),
                    cr3MetDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3MetReason = table.Column<string>(nullable: true),
                    cr3MetUser = table.Column<string>(nullable: true),
                    cr3Note = table.Column<string>(nullable: true),
                    cr3Owner = table.Column<string>(nullable: true),
                    cr3ReceivedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3RequestedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3SignatureFirstName = table.Column<string>(nullable: true),
                    cr3SignatureLastName = table.Column<string>(nullable: true),
                    cr3StartDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3Status = table.Column<string>(nullable: true),
                    cr3StatusChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3StatusChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialRequirement", x => x.cr3Key);
                    table.ForeignKey(
                        name: "FK_CredentialRequirement_Credentials_CredentialCr1Key",
                        column: x => x.CredentialCr1Key,
                        principalTable: "Credentials",
                        principalColumn: "Cr1Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CredentialRequirement_CredentialProcess_CredentialProcesscr2Key",
                        column: x => x.CredentialProcesscr2Key,
                        principalTable: "CredentialProcess",
                        principalColumn: "cr2Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CredentialRequirement_Person_PersonPerKey",
                        column: x => x.PersonPerKey,
                        principalTable: "Person",
                        principalColumn: "PerKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcess_CredentialCr1Key",
                table: "CredentialProcess",
                column: "CredentialCr1Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcess_PersonPerKey",
                table: "CredentialProcess",
                column: "PersonPerKey");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirement_CredentialCr1Key",
                table: "CredentialRequirement",
                column: "CredentialCr1Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirement_CredentialProcesscr2Key",
                table: "CredentialRequirement",
                column: "CredentialProcesscr2Key");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequirement_PersonPerKey",
                table: "CredentialRequirement",
                column: "PersonPerKey");
        }
    }
}
