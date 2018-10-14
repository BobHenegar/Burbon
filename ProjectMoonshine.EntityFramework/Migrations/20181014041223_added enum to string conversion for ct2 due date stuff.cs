using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class addedenumtostringconversionforct2duedatestuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ct2NextCt2key",
                table: "CredentialProcessType",
                newName: "Ct2NextCt2Key");

            migrationBuilder.AlterColumn<string>(
                name: "Ct2DueDateUnit",
                table: "CredentialProcessType",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Ct2DueDateRounding",
                table: "CredentialProcessType",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_CredentialProcessType_Ct2NextCt2Key",
                table: "CredentialProcessType",
                column: "Ct2NextCt2Key");

            migrationBuilder.AddForeignKey(
                name: "FK_CredentialProcessType_CredentialProcessType_Ct2NextCt2Key",
                table: "CredentialProcessType",
                column: "Ct2NextCt2Key",
                principalTable: "CredentialProcessType",
                principalColumn: "Ct2Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CredentialProcessType_CredentialProcessType_Ct2NextCt2Key",
                table: "CredentialProcessType");

            migrationBuilder.DropIndex(
                name: "IX_CredentialProcessType_Ct2NextCt2Key",
                table: "CredentialProcessType");

            migrationBuilder.RenameColumn(
                name: "Ct2NextCt2Key",
                table: "CredentialProcessType",
                newName: "Ct2NextCt2key");

            migrationBuilder.AlterColumn<int>(
                name: "Ct2DueDateUnit",
                table: "CredentialProcessType",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Ct2DueDateRounding",
                table: "CredentialProcessType",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
