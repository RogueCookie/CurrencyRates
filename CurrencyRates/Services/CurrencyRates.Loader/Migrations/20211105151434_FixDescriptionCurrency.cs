using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyRates.Loader.Migrations
{
    public partial class FixDescriptionCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_currency_rates_dailies_currency_id",
                schema: "loader",
                table: "currency_rates_dailies");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 10,
                column: "full_name_of_currency",
                value: "Hungary forint");

            migrationBuilder.CreateIndex(
                name: "ix_currency_rates_dailies_currency_id_provider_id_date_time_cu",
                schema: "loader",
                table: "currency_rates_dailies",
                columns: new[] { "currency_id", "provider_id", "date_time", "currency_base_id", "currency_rate" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_currency_rates_dailies_currency_id_provider_id_date_time_cu",
                schema: "loader",
                table: "currency_rates_dailies");

            migrationBuilder.UpdateData(
                schema: "loader",
                table: "currencies",
                keyColumn: "id",
                keyValue: 10,
                column: "full_name_of_currency",
                value: "Iceland krona");

            migrationBuilder.CreateIndex(
                name: "ix_currency_rates_dailies_currency_id",
                schema: "loader",
                table: "currency_rates_dailies",
                column: "currency_id");
        }
    }
}
