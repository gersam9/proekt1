using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proekt1.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlighteID",
                table: "Flight");

            migrationBuilder.AddColumn<int>(
                name: "FlighteID",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlighteID",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "FlighteID",
                table: "Flight",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
