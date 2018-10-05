using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    LogId = table.Column<Guid>(nullable: false),
                    LogLevel = table.Column<string>(maxLength: 100, nullable: true),
                    Line = table.Column<string>(maxLength: 100, nullable: true),
                    ExceptionType = table.Column<string>(maxLength: 100, nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(maxLength: 100, nullable: true),
                    Method = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.LogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogs");
        }
    }
}
