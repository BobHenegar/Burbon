using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class ColumnChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cr1CstKey",
                table: "Credentials",
                newName: "Cr1PerKey");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Cr1ChangeDate",
                table: "Credentials",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Cr1AddDate",
                table: "Credentials",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Cr1Key",
                table: "Credentials",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonPerKey",
                table: "Credentials",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CredentialProcess",
                columns: table => new
                {
                    cr2Key = table.Column<Guid>(nullable: false),
                    cr2Ct2Process = table.Column<string>(nullable: true),
                    cr2Cr1Key = table.Column<Guid>(nullable: false),
                    cr2PerKey = table.Column<Guid>(nullable: false),
                    cr2Cr2Key = table.Column<Guid>(nullable: false),
                    cr2Status = table.Column<string>(nullable: true),
                    cr2StatusChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2StatusChangeUser = table.Column<string>(nullable: true),
                    cr2StartDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2DueDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2SubmitDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2CompletionDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2TerminationDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2ExtensionDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2ExtensionGrantedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2ExtensionGrantedUser = table.Column<string>(nullable: true),
                    cr2ExtensionGrantedReason = table.Column<string>(nullable: true),
                    cr2Note = table.Column<string>(nullable: true),
                    cr2Owner = table.Column<string>(nullable: true),
                    cr2Due2Date = table.Column<DateTimeOffset>(nullable: false),
                    cr2Extension2Date = table.Column<DateTimeOffset>(nullable: false),
                    cr2Extension2GrantedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2Extension2GrantedUser = table.Column<DateTimeOffset>(nullable: false),
                    cr2Extension2GrantedReason = table.Column<string>(nullable: true),
                    cr2InactiveStartDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2InactiveEndDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2FulfillmentDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2EthicsDisclosure = table.Column<string>(nullable: true),
                    cr2EthicsSummary = table.Column<string>(nullable: true),
                    cr2EthicsClearedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2Agreement = table.Column<string>(nullable: true),
                    cr2AgreementDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2AuditSelectDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2AuditCompleteDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2AuditCompleteUser = table.Column<DateTimeOffset>(nullable: false),
                    cr2DataKey = table.Column<Guid>(nullable: false),
                    cr2ProcessNumber = table.Column<string>(nullable: true),
                    cr2AddUser = table.Column<string>(nullable: true),
                    cr2AddDate = table.Column<DateTimeOffset>(nullable: false),
                    cr2ChangeUser = table.Column<string>(nullable: true),
                    cr2ChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    CredentialCr1Key = table.Column<Guid>(nullable: true),
                    PersonPerKey = table.Column<Guid>(nullable: true)
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
                    cr3Ct3Requirement = table.Column<string>(nullable: true),
                    cr3Cr2Key = table.Column<Guid>(nullable: false),
                    cr3CPerKey = table.Column<Guid>(nullable: false),
                    cr3Status = table.Column<string>(nullable: true),
                    cr3StatusChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3StatusChangeUser = table.Column<string>(nullable: true),
                    cr3StartDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3DueDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3MetDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3MetUser = table.Column<string>(nullable: true),
                    cr3MetReason = table.Column<string>(nullable: true),
                    cr3ExtensionDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3ExtensionGrantedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3ExtensionGrantedUser = table.Column<string>(nullable: true),
                    cr3ExtensionGrantedReason = table.Column<string>(nullable: true),
                    cr3Note = table.Column<string>(nullable: true),
                    cr3Owner = table.Column<string>(nullable: true),
                    cr3Cr3Key = table.Column<Guid>(nullable: false),
                    cr3ChoiceCt3Key = table.Column<Guid>(nullable: false),
                    cr3HideOnlineFlag = table.Column<bool>(nullable: false),
                    cr3DataKey = table.Column<Guid>(nullable: false),
                    cr3AddUser = table.Column<string>(nullable: true),
                    cr3AddDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3ChangeUser = table.Column<string>(nullable: true),
                    cr3ChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3Extension2Date = table.Column<DateTimeOffset>(nullable: false),
                    cr3Extension2GrantedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3Extension2GrantedUser = table.Column<string>(nullable: true),
                    cr3Extension2GrantedReason = table.Column<string>(nullable: true),
                    cr3AttestmentFlag = table.Column<bool>(nullable: false),
                    cr3SignatureFirstName = table.Column<string>(nullable: true),
                    cr3SignatureLastName = table.Column<string>(nullable: true),
                    cr3RequestedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3ReceivedDate = table.Column<DateTimeOffset>(nullable: false),
                    cr3ExamAttempts = table.Column<short>(nullable: false),
                    CredentialCr1Key = table.Column<Guid>(nullable: true),
                    CredentialProcesscr2Key = table.Column<Guid>(nullable: true),
                    PersonPerKey = table.Column<Guid>(nullable: true)
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
                name: "IX_Credentials_PersonPerKey",
                table: "Credentials",
                column: "PersonPerKey");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Person_PersonPerKey",
                table: "Credentials",
                column: "PersonPerKey",
                principalTable: "Person",
                principalColumn: "PerKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Person_PersonPerKey",
                table: "Credentials");

            migrationBuilder.DropTable(
                name: "CredentialRequirement");

            migrationBuilder.DropTable(
                name: "CredentialProcess");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_PersonPerKey",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "PersonPerKey",
                table: "Credentials");

            migrationBuilder.RenameColumn(
                name: "Cr1PerKey",
                table: "Credentials",
                newName: "Cr1CstKey");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Cr1ChangeDate",
                table: "Credentials",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Cr1AddDate",
                table: "Credentials",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "Cr1Key",
                table: "Credentials",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));
        }
    }
}
