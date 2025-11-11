using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuestHouseBookingApplication_Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchemaSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestHouse",
                columns: table => new
                {
                    Guest_House_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guest_House_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Guest_House_Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Guest_House_Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created_By = table.Column<int>(type: "int", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<int>(type: "int", nullable: true),
                    Modification_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active_Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestHouse", x => x.Guest_House_ID);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Room_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guest_House_ID = table.Column<int>(type: "int", nullable: false),
                    Room_No = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Room_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Room_Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_By = table.Column<int>(type: "int", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<int>(type: "int", nullable: true),
                    Modification_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active_Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Room_ID);
                    table.ForeignKey(
                        name: "FK_Room_GuestHouse",
                        column: x => x.Guest_House_ID,
                        principalTable: "GuestHouse",
                        principalColumn: "Guest_House_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bed",
                columns: table => new
                {
                    Bed_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_ID = table.Column<int>(type: "int", nullable: false),
                    Bed_No = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bed_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bed_Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_By = table.Column<int>(type: "int", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<int>(type: "int", nullable: true),
                    Modification_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active_Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bed", x => x.Bed_ID);
                    table.ForeignKey(
                        name: "FK_Bed_Room",
                        column: x => x.Room_ID,
                        principalTable: "Room",
                        principalColumn: "Room_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Booking_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Bed_ID = table.Column<int>(type: "int", nullable: false),
                    Check_In_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Check_Out_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Booking_Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created_By = table.Column<int>(type: "int", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<int>(type: "int", nullable: true),
                    Modification_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active_Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Booking_ID);
                    table.ForeignKey(
                        name: "FK_Booking_Bed",
                        column: x => x.Bed_ID,
                        principalTable: "Bed",
                        principalColumn: "Bed_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_User",
                        column: x => x.User_ID,
                        principalTable: "UserTable",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bed_Room_ID",
                table: "Bed",
                column: "Room_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Bed_ID",
                table: "Booking",
                column: "Bed_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_User_ID",
                table: "Booking",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Guest_House_ID",
                table: "Room",
                column: "Guest_House_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Bed");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "GuestHouse");
        }
    }
}
