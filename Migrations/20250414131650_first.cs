using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proekt1.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EGN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plane",
                columns: table => new
                {
                    PlaneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxSeats = table.Column<int>(type: "int", nullable: false),
                    MaxBusinessSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plane", x => x.PlaneID);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    FlightID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PilotName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaneID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightID);
                    table.ForeignKey(
                        name: "FK_Flight_Plane_PlaneID",
                        column: x => x.PlaneID,
                        principalTable: "Plane",
                        principalColumn: "PlaneID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EGN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaneID = table.Column<int>(type: "int", nullable: false),
                    TicketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightID = table.Column<int>(type: "int", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservation_Flight_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flight",
                        principalColumn: "FlightID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Person_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flight_PlaneID",
                table: "Flight",
                column: "PlaneID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_FlightID",
                table: "Reservation",
                column: "FlightID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserEmail",
                table: "Reservation",
                column: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Plane");
        }
    }
}
