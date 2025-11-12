using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuestHouseBookingApplication_Server.Migrations
{
    /// <inheritdoc />
    public partial class SeperatedAuditColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Date",
                table: "UserTable",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Active_Status",
                table: "UserTable",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deleted_By",
                table: "UserTable",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_Date",
                table: "UserTable",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deleted_By",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_Date",
                table: "Room",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deleted_By",
                table: "GuestHouse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_Date",
                table: "GuestHouse",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deleted_By",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_Date",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deleted_By",
                table: "Bed",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_Date",
                table: "Bed",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted_By",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "Deleted_Date",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "Deleted_By",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Deleted_Date",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Deleted_By",
                table: "GuestHouse");

            migrationBuilder.DropColumn(
                name: "Deleted_Date",
                table: "GuestHouse");

            migrationBuilder.DropColumn(
                name: "Deleted_By",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Deleted_Date",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Deleted_By",
                table: "Bed");

            migrationBuilder.DropColumn(
                name: "Deleted_Date",
                table: "Bed");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_Date",
                table: "UserTable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Active_Status",
                table: "UserTable",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
