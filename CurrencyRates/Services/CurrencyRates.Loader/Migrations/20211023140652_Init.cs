using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CurrencyRates.Loader.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "loader");

            migrationBuilder.CreateTable(
                name: "currencies",
                schema: "loader",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: false),
                    original_country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currencies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "providers",
                schema: "loader",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    provider_name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_providers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currency_rates_dailies",
                schema: "loader",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    currency_base_id = table.Column<int>(type: "integer", nullable: false),
                    currency_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    provider_id = table.Column<int>(type: "integer", nullable: false),
                    currency_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_rates_dailies", x => x.id);
                    table.ForeignKey(
                        name: "fk_currency_rates_dailies_currencies_currency_base_id",
                        column: x => x.currency_base_id,
                        principalSchema: "loader",
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_currency_rates_dailies_currencies_currency_id",
                        column: x => x.currency_id,
                        principalSchema: "loader",
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_currency_rates_dailies_providers_provider_id",
                        column: x => x.provider_id,
                        principalSchema: "loader",
                        principalTable: "providers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "currency_rates_weeklies",
                schema: "loader",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    currency_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    provider_id = table.Column<int>(type: "integer", nullable: false),
                    min_rates_per_week = table.Column<decimal>(type: "numeric", nullable: false),
                    max_rates_per_week = table.Column<decimal>(type: "numeric", nullable: false),
                    currency_base_id = table.Column<int>(type: "integer", nullable: false),
                    currency_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_rates_weeklies", x => x.id);
                    table.ForeignKey(
                        name: "fk_currency_rates_weeklies_currencies_currency_base_id",
                        column: x => x.currency_base_id,
                        principalSchema: "loader",
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_currency_rates_weeklies_currencies_currency_id",
                        column: x => x.currency_id,
                        principalSchema: "loader",
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_currency_rates_weeklies_providers_provider_id",
                        column: x => x.provider_id,
                        principalSchema: "loader",
                        principalTable: "providers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "loader",
                table: "currencies",
                columns: new[] { "id", "alias", "original_country" },
                values: new object[,]
                {
                    { 1, "AUD", null },
                    { 20, "NOK", null },
                    { 21, "PHP", null },
                    { 22, "PLN", null },
                    { 23, "RON", null },
                    { 24, "RUB", null },
                    { 25, "SGD", null },
                    { 19, "NZD", null },
                    { 26, "ZAR", null },
                    { 28, "SEK", null },
                    { 29, "CHF", null },
                    { 30, "THB", null },
                    { 31, "TRY", null },
                    { 32, "GBP", null },
                    { 33, "USD", null },
                    { 27, "KRW", null },
                    { 35, "CZH", null },
                    { 18, "MXN", null },
                    { 16, "JPY", null },
                    { 2, "BRL", null },
                    { 3, "BGN", null },
                    { 4, "CAD", null },
                    { 5, "CNY", null },
                    { 6, "HRK", null },
                    { 7, "DKK", null },
                    { 17, "MYR", null },
                    { 8, "EUR", null },
                    { 10, "HUF", null },
                    { 11, "ISK", null },
                    { 12, "XDR", null },
                    { 13, "INR", null },
                    { 14, "IDR", null },
                    { 15, "ILS", null },
                    { 9, "HKD", null }
                });

            migrationBuilder.InsertData(
                schema: "loader",
                table: "providers",
                columns: new[] { "id", "description", "provider_name" },
                values: new object[] { 1, "Provides currency rates based on Czech crown", "Czech Bank" });

            migrationBuilder.CreateIndex(
                name: "ix_currencies_alias",
                schema: "loader",
                table: "currencies",
                column: "alias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_currency_rates_dailies_currency_base_id",
                schema: "loader",
                table: "currency_rates_dailies",
                column: "currency_base_id");

            migrationBuilder.CreateIndex(
                name: "ix_currency_rates_dailies_currency_id",
                schema: "loader",
                table: "currency_rates_dailies",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_currency_rates_dailies_provider_id",
                schema: "loader",
                table: "currency_rates_dailies",
                column: "provider_id");

            migrationBuilder.CreateIndex(
                name: "ix_currency_rates_weeklies_currency_base_id",
                schema: "loader",
                table: "currency_rates_weeklies",
                column: "currency_base_id");

            migrationBuilder.CreateIndex(
                name: "ix_currency_rates_weeklies_currency_id",
                schema: "loader",
                table: "currency_rates_weeklies",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_currency_rates_weeklies_provider_id",
                schema: "loader",
                table: "currency_rates_weeklies",
                column: "provider_id");

            migrationBuilder.CreateIndex(
                name: "ix_providers_provider_name",
                schema: "loader",
                table: "providers",
                column: "provider_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "currency_rates_dailies",
                schema: "loader");

            migrationBuilder.DropTable(
                name: "currency_rates_weeklies",
                schema: "loader");

            migrationBuilder.DropTable(
                name: "currencies",
                schema: "loader");

            migrationBuilder.DropTable(
                name: "providers",
                schema: "loader");
        }
    }
}
