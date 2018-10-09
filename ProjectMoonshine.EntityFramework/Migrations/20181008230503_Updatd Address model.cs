using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMoonshine.EntityFramework.Migrations
{
    public partial class UpdatdAddressmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PhtAddDate",
                table: "PhoneType",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "PhtKey",
                table: "PhoneType",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PhoAddDate",
                table: "Phone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "PhoKey",
                table: "Phone",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PerAddDate",
                table: "Person",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "PerKey",
                table: "Person",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EltAddDate",
                table: "EmailType",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "EltKey",
                table: "EmailType",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EmlAddDate",
                table: "Email",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmlKey",
                table: "Email",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CusAddDate",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "CusKey",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "AdtAddDate",
                table: "AddressType",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdtKey",
                table: "AddressType",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "AdrAddDate",
                table: "Address",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdrKey",
                table: "Address",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AddDate",
                table: "Address",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "AddUser",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedDate",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUser",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "Address",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "AddUser",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ModifiedUser",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "Address");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PhtAddDate",
                table: "PhoneType",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "PhtKey",
                table: "PhoneType",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PhoAddDate",
                table: "Phone",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "PhoKey",
                table: "Phone",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PerAddDate",
                table: "Person",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "PerKey",
                table: "Person",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EltAddDate",
                table: "EmailType",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "EltKey",
                table: "EmailType",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EmlAddDate",
                table: "Email",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "EmlKey",
                table: "Email",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CusAddDate",
                table: "Customer",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "CusKey",
                table: "Customer",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "AdtAddDate",
                table: "AddressType",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "AdtKey",
                table: "AddressType",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "AdrAddDate",
                table: "Address",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<Guid>(
                name: "AdrKey",
                table: "Address",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));
        }
    }
}
