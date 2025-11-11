using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuestHouseBookingApplication_Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllEntitySchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guest_House_Location",
                table: "GuestHouse");

            migrationBuilder.AlterColumn<string>(
                name: "Guest_House_Name",
                table: "GuestHouse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Contact_Email",
                table: "GuestHouse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Contact_Number",
                table: "GuestHouse",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuestHouse_Created_By",
                table: "GuestHouse",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_GuestHouse_Modified_By",
                table: "GuestHouse",
                column: "Modified_By");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestHouse_CreatedBy_UserTable",
                table: "GuestHouse",
                column: "Created_By",
                principalTable: "UserTable",
                principalColumn: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestHouse_ModifiedBy_UserTable",
                table: "GuestHouse",
                column: "Modified_By",
                principalTable: "UserTable",
                principalColumn: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestHouse_CreatedBy_UserTable",
                table: "GuestHouse");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestHouse_ModifiedBy_UserTable",
                table: "GuestHouse");

            migrationBuilder.DropIndex(
                name: "IX_GuestHouse_Created_By",
                table: "GuestHouse");

            migrationBuilder.DropIndex(
                name: "IX_GuestHouse_Modified_By",
                table: "GuestHouse");

            migrationBuilder.DropColumn(
                name: "Contact_Email",
                table: "GuestHouse");

            migrationBuilder.DropColumn(
                name: "Contact_Number",
                table: "GuestHouse");

            migrationBuilder.AlterColumn<string>(
                name: "Guest_House_Name",
                table: "GuestHouse",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "Guest_House_Location",
                table: "GuestHouse",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
