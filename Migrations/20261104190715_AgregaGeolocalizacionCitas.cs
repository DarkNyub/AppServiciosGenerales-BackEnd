using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AgregaGeolocalizacionCitas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LatitudServicio",
                table: "Citas",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LatitudSolicitud",
                table: "Citas",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LongitudServicio",
                table: "Citas",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LongitudSolicitud",
                table: "Citas",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatitudServicio",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "LatitudSolicitud",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "LongitudServicio",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "LongitudSolicitud",
                table: "Citas");
        }
    }
}
