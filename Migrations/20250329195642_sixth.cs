using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proekt1.Migrations
{
    /// <inheritdoc />
    public partial class sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Flight_FlightID",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "FlighteID",
                table: "Reservation");

            migrationBuilder.AlterColumn<int>(
                name: "FlightID",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Flight_FlightID",
                table: "Reservation",
                column: "FlightID",
                principalTable: "Flight",
                principalColumn: "FlightID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Flight_FlightID",
                table: "Reservation");

            migrationBuilder.AlterColumn<int>(
                name: "FlightID",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FlighteID",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Flight_FlightID",
                table: "Reservation",
                column: "FlightID",
                principalTable: "Flight",
                principalColumn: "FlightID");
        }
    }
}
