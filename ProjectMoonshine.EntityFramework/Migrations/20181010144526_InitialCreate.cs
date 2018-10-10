using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class InitialCreate : Migration
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

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CusKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CusAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    CusChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    CusAddUser = table.Column<string>(nullable: true),
                    CusChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CusKey);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PerKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    PerCusKey = table.Column<Guid>(nullable: false),
                    PerFirstName = table.Column<string>(nullable: true),
                    PerMiddleName = table.Column<string>(nullable: true),
                    PerLastName = table.Column<string>(nullable: true),
                    PerDateOfBirth = table.Column<DateTimeOffset>(nullable: false),
                    PerGender = table.Column<string>(nullable: true),
                    PerAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    PerChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    PerAddUser = table.Column<string>(nullable: true),
                    PerChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PerKey);
                    table.ForeignKey(
                        name: "FK_Person_Customer_PerCusKey",
                        column: x => x.PerCusKey,
                        principalTable: "Customer",
                        principalColumn: "CusKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AdrKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    AdrPerKey = table.Column<Guid>(nullable: false),
                    AdrCity = table.Column<string>(maxLength: 50, nullable: true),
                    AdrState = table.Column<string>(maxLength: 75, nullable: true),
                    AdrZip = table.Column<string>(maxLength: 10, nullable: true),
                    AdrAddress1 = table.Column<string>(maxLength: 500, nullable: true),
                    AdrAddress2 = table.Column<string>(maxLength: 500, nullable: true),
                    AdrAddress3 = table.Column<string>(maxLength: 500, nullable: true),
                    AdrIsPrimaryAddress = table.Column<bool>(nullable: false),
                    AdrIsMailingAddress = table.Column<bool>(nullable: false),
                    AdrIsWorkAddress = table.Column<bool>(nullable: false),
                    AdrIsHomeAddress = table.Column<bool>(nullable: false),
                    AdrAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    AdrChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    AdrAddUser = table.Column<string>(nullable: true),
                    AdrChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AdrKey);
                    table.ForeignKey(
                        name: "FK_Address_Person_AdrPerKey",
                        column: x => x.AdrPerKey,
                        principalTable: "Person",
                        principalColumn: "PerKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Cr1Key = table.Column<Guid>(nullable: false),
                    Cr1Ct1Credential = table.Column<string>(nullable: true),
                    Cr1CredentialNumber = table.Column<string>(nullable: true),
                    Cr1PerKey = table.Column<Guid>(nullable: false),
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
                    Cr1AddUser = table.Column<string>(nullable: true),
                    Cr1AddDate = table.Column<DateTimeOffset>(nullable: false),
                    Cr1ChangeUser = table.Column<string>(nullable: true),
                    Cr1ChangeDate = table.Column<DateTimeOffset>(nullable: false),
                    Cr1LegalFirstName = table.Column<string>(nullable: true),
                    Cr1LegalLastName = table.Column<string>(nullable: true),
                    PersonPerKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Cr1Key);
                    table.ForeignKey(
                        name: "FK_Credentials_Person_PersonPerKey",
                        column: x => x.PersonPerKey,
                        principalTable: "Person",
                        principalColumn: "PerKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    EmlKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    EmlPerKey = table.Column<Guid>(nullable: false),
                    EmlAddress = table.Column<string>(nullable: true),
                    EmlIsPrimaryEmail = table.Column<bool>(nullable: false),
                    EmlIsWorkEmail = table.Column<bool>(nullable: false),
                    EmlIsPersonalEmail = table.Column<bool>(nullable: false),
                    EmlAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    EmlChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    EmlAddUser = table.Column<string>(nullable: true),
                    EmlChangeUser = table.Column<string>(nullable: true),
                    PersonPerKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.EmlKey);
                    table.ForeignKey(
                        name: "FK_Email_Person_PersonPerKey",
                        column: x => x.PersonPerKey,
                        principalTable: "Person",
                        principalColumn: "PerKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    PhoKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    PhoPerKey = table.Column<Guid>(nullable: false),
                    PhoNumber = table.Column<string>(nullable: true),
                    PhoExtention = table.Column<string>(nullable: true),
                    PhoIsPrimary = table.Column<bool>(nullable: false),
                    PhoIsHome = table.Column<bool>(nullable: false),
                    PhoIsMobile = table.Column<bool>(nullable: false),
                    PhoIsWork = table.Column<bool>(nullable: false),
                    PhoAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    PhoChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    PhoAddUser = table.Column<string>(nullable: true),
                    PhoChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.PhoKey);
                    table.ForeignKey(
                        name: "FK_Phone_Person_PhoPerKey",
                        column: x => x.PhoPerKey,
                        principalTable: "Person",
                        principalColumn: "PerKey",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Address_AdrPerKey",
                table: "Address",
                column: "AdrPerKey");

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

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_PersonPerKey",
                table: "Credentials",
                column: "PersonPerKey");

            migrationBuilder.CreateIndex(
                name: "IX_Email_PersonPerKey",
                table: "Email",
                column: "PersonPerKey");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PerCusKey",
                table: "Person",
                column: "PerCusKey");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_PhoPerKey",
                table: "Phone",
                column: "PhoPerKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CredentialRequirement");

            migrationBuilder.DropTable(
                name: "CredentialType");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "CredentialProcess");

            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
