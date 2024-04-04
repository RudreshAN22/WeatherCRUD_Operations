using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherCRUD_Operations.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SummaryId",
                table: "WeatherForecastingSummary",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherForecastingSummary",
                table: "WeatherForecastingSummary",
                column: "SummaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherForecastingSummary",
                table: "WeatherForecastingSummary");

            migrationBuilder.DropColumn(
                name: "SummaryId",
                table: "WeatherForecastingSummary");
        }
    }
}
