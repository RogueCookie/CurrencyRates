using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyRates.Loader.Migrations
{
    public partial class AddedFullNameCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "full_name_of_currency",
                schema: "loader",
                table: "currencies",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 1,
                column: "full_name_of_currency",
                value: "Australia dollar");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 2,
                column: "full_name_of_currency",
                value: "Brazil real");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 3,
                column: "full_name_of_currency",
                value: "Bulgaria lev");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 4,
                column: "full_name_of_currency",
                value: "Canada dollar");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 5,
                column: "full_name_of_currency",
                value: "China renminbi");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 6,
                column: "full_name_of_currency",
                value: "Croatia kuna");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 7,
                column: "full_name_of_currency",
                value: "Denmark krone");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 8,
                column: "full_name_of_currency",
                value: "EMU euro");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 9,
                column: "full_name_of_currency",
                value: "Hongkong dollar");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 10,
                column: "full_name_of_currency",
                value: "Iceland krona");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 11,
                column: "full_name_of_currency",
                value: "Iceland krona");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 12,
                column: "full_name_of_currency",
                value: "IMF SDR");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 13,
                column: "full_name_of_currency",
                value: "India rupee");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 14,
                column: "full_name_of_currency",
                value: "Indonesia rupiah");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 15,
                column: "full_name_of_currency",
                value: "Israel new shekel");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 16,
                column: "full_name_of_currency",
                value: "Japan yen");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 17,
                column: "full_name_of_currency",
                value: "Malaysia ringgit");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 18,
                column: "full_name_of_currency",
                value: "Mexico peso");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 19,
                column: "full_name_of_currency",
                value: "New Zealand dollar");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 20,
                column: "full_name_of_currency",
                value: "Norway krone");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 21,
                column: "full_name_of_currency",
                value: "Philippines peso");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 22,
                column: "full_name_of_currency",
                value: "Poland zloty");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 23,
                column: "full_name_of_currency",
                value: "Romania leu");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 24,
                column: "full_name_of_currency",
                value: "Russia rouble");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 25,
                column: "full_name_of_currency",
                value: "Singapore dollar");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 26,
                column: "full_name_of_currency",
                value: "South Africa rand");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 27,
                column: "full_name_of_currency",
                value: "South Korea won");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 28,
                column: "full_name_of_currency",
                value: "Sweden krona");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 29,
                column: "full_name_of_currency",
                value: "Switzerland franc");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 30,
                column: "full_name_of_currency",
                value: "Thailand baht");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 31,
                column: "full_name_of_currency",
                value: "Turkey lira");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 32,
                column: "full_name_of_currency",
                value: "United Kingdom pound");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 33,
                column: "full_name_of_currency",
                value: "USA dollar");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 35,
                column: "full_name_of_currency",
                value: "Czech crown");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "full_name_of_currency",
                schema: "loader",
                table: "currencies");
        }
    }
}
