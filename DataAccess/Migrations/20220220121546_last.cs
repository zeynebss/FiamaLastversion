using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1023",
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 2, 20, 4, 15, 45, 146, DateTimeKind.Unspecified).AddTicks(4228), new TimeSpan(0, -8, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1023",
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 2, 19, 6, 39, 33, 690, DateTimeKind.Unspecified).AddTicks(9182), new TimeSpan(0, -8, 0, 0, 0)));
        }
    }
}
