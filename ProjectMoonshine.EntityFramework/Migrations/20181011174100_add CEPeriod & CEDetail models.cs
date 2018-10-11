using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addCEPeriodCEDetailmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CEDetail",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Ce1Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Ce1CusKey = table.Column<Guid>(nullable: false),
                    Ce1ActivityDate = table.Column<DateTimeOffset>(nullable: false),
                    Ce1ActivityDescription = table.Column<string>(nullable: false),
                    Ce1Units = table.Column<float>(nullable: false),
                    Ce1AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Ce1ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Ce1AddUser = table.Column<string>(nullable: true),
                    Ce1ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEDetail", x => x.Ce1Key);
                    table.ForeignKey(
                        name: "FK_CEDetail_Customer_Ce1CusKey",
                        column: x => x.Ce1CusKey,
                        principalTable: "Customer",
                        principalColumn: "CusKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CEPeriod",
                columns: table => new
                {
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Ce2Key = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Ce2CusKey = table.Column<Guid>(nullable: false),
                    Ce2StartDate = table.Column<DateTimeOffset>(nullable: false),
                    Ce2EndDate = table.Column<DateTimeOffset>(nullable: false),
                    Ce2AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    Ce2ChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    Ce2AddUser = table.Column<string>(nullable: true),
                    Ce2ChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEPeriod", x => x.Ce2Key);
                    table.ForeignKey(
                        name: "FK_CEPeriod_Customer_Ce2CusKey",
                        column: x => x.Ce2CusKey,
                        principalTable: "Customer",
                        principalColumn: "CusKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CEDetail_Ce1CusKey",
                table: "CEDetail",
                column: "Ce1CusKey");

            migrationBuilder.CreateIndex(
                name: "IX_CEPeriod_Ce2CusKey",
                table: "CEPeriod",
                column: "Ce2CusKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CEDetail");

            migrationBuilder.DropTable(
                name: "CEPeriod");
        }
    }
}
