using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddedFileUploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileUploads",
                columns: table => new
                {
                    FupKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    FupPerKey = table.Column<Guid>(nullable: false),
                    FupFileData = table.Column<byte[]>(nullable: true),
                    FupFileType = table.Column<string>(nullable: true),
                    FupFileName = table.Column<string>(nullable: true),
                    FupFileDescription = table.Column<string>(nullable: true),
                    FupAddUser = table.Column<string>(nullable: true),
                    FupChangeUser = table.Column<string>(nullable: true),
                    FupAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    FupChangeDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploads", x => x.FupKey);
                    table.ForeignKey(
                        name: "FK_FileUploads_Person_FupPerKey",
                        column: x => x.FupPerKey,
                        principalTable: "Person",
                        principalColumn: "PerKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_FupPerKey",
                table: "FileUploads",
                column: "FupPerKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileUploads");
        }
    }
}
