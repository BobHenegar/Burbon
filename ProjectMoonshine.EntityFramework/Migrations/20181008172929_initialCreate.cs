using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressType",
                columns: table => new
                {
                    AdtKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    AdtType = table.Column<string>(maxLength: 10, nullable: true),
                    AdtAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    AdtAddUser = table.Column<string>(nullable: true),
                    AdtChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    AdtChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressType", x => x.AdtKey);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CusKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CusPerKey = table.Column<Guid>(nullable: true),
                    CusAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    CusAddUser = table.Column<string>(nullable: true),
                    CusChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    CusChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CusKey);
                });

            migrationBuilder.CreateTable(
                name: "EmailType",
                columns: table => new
                {
                    EltKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    EltType = table.Column<string>(maxLength: 10, nullable: true),
                    EltAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    EltAddUser = table.Column<string>(nullable: true),
                    EltChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    EltChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailType", x => x.EltKey);
                });

            migrationBuilder.CreateTable(
                name: "PhoneType",
                columns: table => new
                {
                    PhtKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    PhtType = table.Column<string>(nullable: true),
                    PhtAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    PhtAddUser = table.Column<string>(nullable: true),
                    PhtChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    PhtChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneType", x => x.PhtKey);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PerKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CusPerKey = table.Column<Guid>(nullable: false),
                    PerFirstName = table.Column<string>(nullable: true),
                    PerMiddleName = table.Column<string>(nullable: true),
                    PerLastName = table.Column<string>(nullable: true),
                    PerDateOfBirth = table.Column<DateTimeOffset>(nullable: false),
                    PerGender = table.Column<string>(nullable: true),
                    PerAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    PerAddUser = table.Column<string>(nullable: true),
                    PerChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    PerChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PerKey);
                    table.ForeignKey(
                        name: "FK_Person_Customer_CusPerKey",
                        column: x => x.CusPerKey,
                        principalTable: "Customer",
                        principalColumn: "CusKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AdrKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    AdrPerKey = table.Column<Guid>(nullable: false),
                    AdrCity = table.Column<string>(nullable: true),
                    AdrState = table.Column<string>(nullable: true),
                    AdrZip = table.Column<string>(nullable: true),
                    AdrAddress1 = table.Column<string>(nullable: true),
                    AdrAddress2 = table.Column<string>(nullable: true),
                    AdrAddress3 = table.Column<string>(nullable: true),
                    AdtAddressTypeAdtKey = table.Column<Guid>(nullable: true),
                    AdrAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    AdrAddUser = table.Column<string>(nullable: true),
                    AdrChangeDate = table.Column<DateTimeOffset>(nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_AddressType_AdtAddressTypeAdtKey",
                        column: x => x.AdtAddressTypeAdtKey,
                        principalTable: "AddressType",
                        principalColumn: "AdtKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    EmlKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    EmlEltKey = table.Column<Guid>(nullable: true),
                    EmlAddress = table.Column<string>(nullable: true),
                    EmlPrimary = table.Column<bool>(nullable: false),
                    EmlAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    EmlAddUser = table.Column<string>(nullable: true),
                    EmlChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    EmlChangeUser = table.Column<string>(nullable: true),
                    PersonPerKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.EmlKey);
                    table.ForeignKey(
                        name: "FK_Email_EmailType_EmlEltKey",
                        column: x => x.EmlEltKey,
                        principalTable: "EmailType",
                        principalColumn: "EltKey",
                        onDelete: ReferentialAction.Restrict);
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
                    PhoPerKey = table.Column<Guid>(nullable: true),
                    PhoNumber = table.Column<string>(nullable: true),
                    PhoExtention = table.Column<string>(nullable: true),
                    PhoPrimary = table.Column<bool>(nullable: false),
                    PhoneTypePhtKey = table.Column<Guid>(nullable: true),
                    PersonPerKey = table.Column<Guid>(nullable: true),
                    PhoAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    PhoAddUser = table.Column<string>(nullable: true),
                    PhoChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    PhoChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.PhoKey);
                    table.ForeignKey(
                        name: "FK_Phone_Person_PersonPerKey",
                        column: x => x.PersonPerKey,
                        principalTable: "Person",
                        principalColumn: "PerKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Phone_PhoneType_PhoneTypePhtKey",
                        column: x => x.PhoneTypePhtKey,
                        principalTable: "PhoneType",
                        principalColumn: "PhtKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_AdrPerKey",
                table: "Address",
                column: "AdrPerKey");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AdtAddressTypeAdtKey",
                table: "Address",
                column: "AdtAddressTypeAdtKey");

            migrationBuilder.CreateIndex(
                name: "IX_Email_EmlEltKey",
                table: "Email",
                column: "EmlEltKey");

            migrationBuilder.CreateIndex(
                name: "IX_Email_PersonPerKey",
                table: "Email",
                column: "PersonPerKey");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CusPerKey",
                table: "Person",
                column: "CusPerKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phone_PersonPerKey",
                table: "Phone",
                column: "PersonPerKey");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_PhoneTypePhtKey",
                table: "Phone",
                column: "PhoneTypePhtKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "AddressType");

            migrationBuilder.DropTable(
                name: "EmailType");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "PhoneType");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
