using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViajesFast.Migrations
{
    /// <inheritdoc />
    public partial class actualizacionA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aerolinea",
                table: "Vuelos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aerolinea",
                table: "Vuelos");
        }
    }
}
