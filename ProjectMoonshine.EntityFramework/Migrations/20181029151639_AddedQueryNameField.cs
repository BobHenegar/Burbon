using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class AddedQueryNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Queries",
                columns: table => new
                {
                    QryKey = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    QryJsonBody = table.Column<string>(nullable: false),
                    QryName = table.Column<string>(nullable: false),
                    QryAddDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    QryChangeDate = table.Column<DateTimeOffset>(nullable: true),
                    QryAddUser = table.Column<string>(nullable: true),
                    QryChangeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queries", x => x.QryKey);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queries");
        }
    }
}
