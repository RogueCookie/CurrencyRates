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
                name: "Currencies",
                schema: "loader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrencyAlias = table.Column<string>(type: "text", nullable: true, comment: " Shortcut of currency")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                schema: "loader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProviderName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRatesDailies",
                schema: "loader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrencyBaseId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "numeric", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRatesDailies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyRatesDailies_Currencies_CurrencyBaseId",
                        column: x => x.CurrencyBaseId,
                        principalSchema: "loader",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyRatesDailies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "loader",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyRatesDailies_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "loader",
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRatesWeeklies",
                schema: "loader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrencyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    MinRatesPerWeek = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxRatesPerWeek = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrencyBaseId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRatesWeeklies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyRatesWeeklies_Currencies_CurrencyBaseId",
                        column: x => x.CurrencyBaseId,
                        principalSchema: "loader",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyRatesWeeklies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "loader",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyRatesWeeklies_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "loader",
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRatesDailies_CurrencyBaseId",
                schema: "loader",
                table: "CurrencyRatesDailies",
                column: "CurrencyBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRatesDailies_CurrencyId",
                schema: "loader",
                table: "CurrencyRatesDailies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRatesDailies_ProviderId",
                schema: "loader",
                table: "CurrencyRatesDailies",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRatesWeeklies_CurrencyBaseId",
                schema: "loader",
                table: "CurrencyRatesWeeklies",
                column: "CurrencyBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRatesWeeklies_CurrencyId",
                schema: "loader",
                table: "CurrencyRatesWeeklies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRatesWeeklies_ProviderId",
                schema: "loader",
                table: "CurrencyRatesWeeklies",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRatesDailies",
                schema: "loader");

            migrationBuilder.DropTable(
                name: "CurrencyRatesWeeklies",
                schema: "loader");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "loader");

            migrationBuilder.DropTable(
                name: "Providers",
                schema: "loader");
        }
    }
}
