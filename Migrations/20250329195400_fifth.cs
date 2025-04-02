using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proekt1.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightID",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_FlightID",
                table: "Reservation",
                column: "FlightID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Flight_FlightID",
                table: "Reservation",
                column: "FlightID",
                principalTable: "Flight",
                principalColumn: "FlightID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Flight_FlightID",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_FlightID",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "FlightID",
                table: "Reservation");
        }
    }
}
