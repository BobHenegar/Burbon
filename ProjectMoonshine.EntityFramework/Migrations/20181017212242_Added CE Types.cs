using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddedCETypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ce1CetKey",
                table: "CEDetail",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CEType",
                columns: table => new
                {
                    CetKey = table.Column<Guid>(nullable: false),
                    CetCode = table.Column<string>(nullable: false),
                    CetDescription = table.Column<string>(nullable: true),
                    CetAddDate = table.Column<DateTimeOffset>(nullable: false),
                    CetChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    CetAddUser = table.Column<string>(nullable: true),
                    CetChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEType", x => x.CetKey);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CEDetail_Ce1CetKey",
                table: "CEDetail",
                column: "Ce1CetKey");

            migrationBuilder.AddForeignKey(
                name: "FK_CEDetail_CEType_Ce1CetKey",
                table: "CEDetail",
                column: "Ce1CetKey",
                principalTable: "CEType",
                principalColumn: "CetKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CEDetail_CEType_Ce1CetKey",
                table: "CEDetail");

            migrationBuilder.DropTable(
                name: "CEType");

            migrationBuilder.DropIndex(
                name: "IX_CEDetail_Ce1CetKey",
                table: "CEDetail");

            migrationBuilder.DropColumn(
                name: "Ce1CetKey",
                table: "CEDetail");
        }
    }
}
