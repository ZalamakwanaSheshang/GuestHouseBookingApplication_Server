using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuestHouseBookingApplication_Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password_Hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password_Sault = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email_ID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Contact_Number = table.Column<long>(type: "bigint", nullable: true),
                    First_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<int>(type: "int", nullable: true),
                    Modification_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<int>(type: "int", nullable: true),
                    Active_Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.User_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTable");
        }
    }
}
