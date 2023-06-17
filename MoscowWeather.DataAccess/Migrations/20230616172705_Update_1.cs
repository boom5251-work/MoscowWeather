using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoscowWeather.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WeatherConditions_DateTime",
                table: "WeatherConditions",
                column: "DateTime",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WeatherConditions_DateTime",
                table: "WeatherConditions");
        }
    }
}
