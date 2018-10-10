using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
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
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    PerKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    PerCusKey = table.Column<Guid>(nullable: false),
                    PerFirstName = table.Column<string>(nullable: false),
                    PerMiddleName = table.Column<string>(nullable: true),
                    PerLastName = table.Column<string>(nullable: false),
                    PerDateOfBirth = table.Column<DateTimeOffset>(nullable: false),
                    PerGender = table.Column<string>(nullable: false),
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
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
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
                name: "Email",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
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
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    PhoKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    PhoPerKey = table.Column<Guid>(nullable: false),
                    PhoNumber = table.Column<string>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Address_AdrPerKey",
                table: "Address",
                column: "AdrPerKey");

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
                name: "Email");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
