using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddedCommTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommunicationTemplate",
                columns: table => new
                {
                    TemKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    TemCode = table.Column<string>(nullable: true),
                    TemDescription = table.Column<string>(nullable: true),
                    TemHtml = table.Column<string>(nullable: true),
                    TemText = table.Column<string>(nullable: true),
                    TemAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    TemChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    TemAddUser = table.Column<string>(nullable: true),
                    TemChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTemplate", x => x.TemKey);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunicationTemplate");
        }
    }
}
