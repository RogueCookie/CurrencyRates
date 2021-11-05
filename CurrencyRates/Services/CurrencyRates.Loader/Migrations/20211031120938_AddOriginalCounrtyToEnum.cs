using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyRates.Loader.Migrations
{
    public partial class AddOriginalCounrtyToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 1,
                column: "original_country",
                value: "Australia");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 2,
                column: "original_country",
                value: "Brazil");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 3,
                column: "original_country",
                value: "Bulgaria");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 4,
                column: "original_country",
                value: "Canada");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 5,
                column: "original_country",
                value: "China");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 6,
                column: "original_country",
                value: "Croatia");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 7,
                column: "original_country",
                value: "Denmark");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 8,
                column: "original_country",
                value: "EMU");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 9,
                column: "original_country",
                value: "Hongkong");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 10,
                column: "original_country",
                value: "Hungary");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 11,
                column: "original_country",
                value: "Iceland");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 12,
                column: "original_country",
                value: "IMF");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 13,
                column: "original_country",
                value: "India");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 14,
                column: "original_country",
                value: "Indonesia");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 15,
                column: "original_country",
                value: "Israel");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 16,
                column: "original_country",
                value: "Japan");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 17,
                column: "original_country",
                value: "Malaysia");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 18,
                column: "original_country",
                value: "Mexico");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 19,
                column: "original_country",
                value: "New Zealand");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 20,
                column: "original_country",
                value: "Norway");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 21,
                column: "original_country",
                value: "Philippines");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 22,
                column: "original_country",
                value: "Poland");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 23,
                column: "original_country",
                value: "Romania");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 24,
                column: "original_country",
                value: "Russia");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 25,
                column: "original_country",
                value: "Singapore");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 26,
                column: "original_country",
                value: "South Africa");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 27,
                column: "original_country",
                value: "South Korea");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 28,
                column: "original_country",
                value: "Sweden");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 29,
                column: "original_country",
                value: "Switzerland");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 30,
                column: "original_country",
                value: "Thailand");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 31,
                column: "original_country",
                value: "Turkey");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 32,
                column: "original_country",
                value: "United Kingdom");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 33,
                column: "original_country",
                value: "USA");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 35,
                column: "original_country",
                value: "Czech Republic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 1,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 2,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 3,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 4,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 5,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 6,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 7,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 8,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 9,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 10,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 11,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 12,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 13,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 14,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 15,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 16,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 17,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 18,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 19,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 20,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 21,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 22,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 23,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 24,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 25,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 26,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 27,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 28,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 29,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 30,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 31,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 32,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 33,
                column: "original_country",
                value: null);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 35,
                column: "original_country",
                value: null);
        }
    }
}
