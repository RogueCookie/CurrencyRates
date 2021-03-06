// <auto-generated />
using System;
using CurrencyRates.Loader.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CurrencyRates.Loader.Migrations
{
    [DbContext(typeof(LoaderContext))]
    [Migration("20211031123233_AddedFullNameCurrency")]
    partial class AddedFullNameCurrency
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("loader")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CurrencyRates.Loader.DAL.Model.Currency", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("alias");

                    b.Property<string>("FullNameOfCurrency")
                        .HasColumnType("text")
                        .HasColumnName("full_name_of_currency");

                    b.Property<string>("OriginalCountry")
                        .HasColumnType("text")
                        .HasColumnName("original_country");

                    b.HasKey("Id")
                        .HasName("pk_currencies");

                    b.HasIndex("Alias")
                        .IsUnique()
                        .HasDatabaseName("ix_currencies_alias");

                    b.ToTable("currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alias = "AUD",
                            FullNameOfCurrency = "Australia dollar",
                            OriginalCountry = "Australia"
                        },
                        new
                        {
                            Id = 2,
                            Alias = "BRL",
                            FullNameOfCurrency = "Brazil real",
                            OriginalCountry = "Brazil"
                        },
                        new
                        {
                            Id = 3,
                            Alias = "BGN",
                            FullNameOfCurrency = "Bulgaria lev",
                            OriginalCountry = "Bulgaria"
                        },
                        new
                        {
                            Id = 4,
                            Alias = "CAD",
                            FullNameOfCurrency = "Canada dollar",
                            OriginalCountry = "Canada"
                        },
                        new
                        {
                            Id = 5,
                            Alias = "CNY",
                            FullNameOfCurrency = "China renminbi",
                            OriginalCountry = "China"
                        },
                        new
                        {
                            Id = 6,
                            Alias = "HRK",
                            FullNameOfCurrency = "Croatia kuna",
                            OriginalCountry = "Croatia"
                        },
                        new
                        {
                            Id = 7,
                            Alias = "DKK",
                            FullNameOfCurrency = "Denmark krone",
                            OriginalCountry = "Denmark"
                        },
                        new
                        {
                            Id = 8,
                            Alias = "EUR",
                            FullNameOfCurrency = "EMU euro",
                            OriginalCountry = "EMU"
                        },
                        new
                        {
                            Id = 9,
                            Alias = "HKD",
                            FullNameOfCurrency = "Hongkong dollar",
                            OriginalCountry = "Hongkong"
                        },
                        new
                        {
                            Id = 10,
                            Alias = "HUF",
                            FullNameOfCurrency = "Iceland krona",
                            OriginalCountry = "Hungary"
                        },
                        new
                        {
                            Id = 11,
                            Alias = "ISK",
                            FullNameOfCurrency = "Iceland krona",
                            OriginalCountry = "Iceland"
                        },
                        new
                        {
                            Id = 12,
                            Alias = "XDR",
                            FullNameOfCurrency = "IMF SDR",
                            OriginalCountry = "IMF"
                        },
                        new
                        {
                            Id = 13,
                            Alias = "INR",
                            FullNameOfCurrency = "India rupee",
                            OriginalCountry = "India"
                        },
                        new
                        {
                            Id = 14,
                            Alias = "IDR",
                            FullNameOfCurrency = "Indonesia rupiah",
                            OriginalCountry = "Indonesia"
                        },
                        new
                        {
                            Id = 15,
                            Alias = "ILS",
                            FullNameOfCurrency = "Israel new shekel",
                            OriginalCountry = "Israel"
                        },
                        new
                        {
                            Id = 16,
                            Alias = "JPY",
                            FullNameOfCurrency = "Japan yen",
                            OriginalCountry = "Japan"
                        },
                        new
                        {
                            Id = 17,
                            Alias = "MYR",
                            FullNameOfCurrency = "Malaysia ringgit",
                            OriginalCountry = "Malaysia"
                        },
                        new
                        {
                            Id = 18,
                            Alias = "MXN",
                            FullNameOfCurrency = "Mexico peso",
                            OriginalCountry = "Mexico"
                        },
                        new
                        {
                            Id = 19,
                            Alias = "NZD",
                            FullNameOfCurrency = "New Zealand dollar",
                            OriginalCountry = "New Zealand"
                        },
                        new
                        {
                            Id = 20,
                            Alias = "NOK",
                            FullNameOfCurrency = "Norway krone",
                            OriginalCountry = "Norway"
                        },
                        new
                        {
                            Id = 21,
                            Alias = "PHP",
                            FullNameOfCurrency = "Philippines peso",
                            OriginalCountry = "Philippines"
                        },
                        new
                        {
                            Id = 22,
                            Alias = "PLN",
                            FullNameOfCurrency = "Poland zloty",
                            OriginalCountry = "Poland"
                        },
                        new
                        {
                            Id = 23,
                            Alias = "RON",
                            FullNameOfCurrency = "Romania leu",
                            OriginalCountry = "Romania"
                        },
                        new
                        {
                            Id = 24,
                            Alias = "RUB",
                            FullNameOfCurrency = "Russia rouble",
                            OriginalCountry = "Russia"
                        },
                        new
                        {
                            Id = 25,
                            Alias = "SGD",
                            FullNameOfCurrency = "Singapore dollar",
                            OriginalCountry = "Singapore"
                        },
                        new
                        {
                            Id = 26,
                            Alias = "ZAR",
                            FullNameOfCurrency = "South Africa rand",
                            OriginalCountry = "South Africa"
                        },
                        new
                        {
                            Id = 27,
                            Alias = "KRW",
                            FullNameOfCurrency = "South Korea won",
                            OriginalCountry = "South Korea"
                        },
                        new
                        {
                            Id = 28,
                            Alias = "SEK",
                            FullNameOfCurrency = "Sweden krona",
                            OriginalCountry = "Sweden"
                        },
                        new
                        {
                            Id = 29,
                            Alias = "CHF",
                            FullNameOfCurrency = "Switzerland franc",
                            OriginalCountry = "Switzerland"
                        },
                        new
                        {
                            Id = 30,
                            Alias = "THB",
                            FullNameOfCurrency = "Thailand baht",
                            OriginalCountry = "Thailand"
                        },
                        new
                        {
                            Id = 31,
                            Alias = "TRY",
                            FullNameOfCurrency = "Turkey lira",
                            OriginalCountry = "Turkey"
                        },
                        new
                        {
                            Id = 32,
                            Alias = "GBP",
                            FullNameOfCurrency = "United Kingdom pound",
                            OriginalCountry = "United Kingdom"
                        },
                        new
                        {
                            Id = 33,
                            Alias = "USD",
                            FullNameOfCurrency = "USA dollar",
                            OriginalCountry = "USA"
                        },
                        new
                        {
                            Id = 35,
                            Alias = "CZH",
                            FullNameOfCurrency = "Czech crown",
                            OriginalCountry = "Czech Republic"
                        });
                });

            modelBuilder.Entity("CurrencyRates.Loader.DAL.Model.CurrencyRatesDaily", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CurrencyBaseId")
                        .HasColumnType("integer")
                        .HasColumnName("currency_base_id");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("currency_id");

                    b.Property<decimal>("CurrencyRate")
                        .HasColumnType("numeric")
                        .HasColumnName("currency_rate");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_time");

                    b.Property<int>("ProviderId")
                        .HasColumnType("integer")
                        .HasColumnName("provider_id");

                    b.HasKey("Id")
                        .HasName("pk_currency_rates_dailies");

                    b.HasIndex("CurrencyBaseId")
                        .HasDatabaseName("ix_currency_rates_dailies_currency_base_id");

                    b.HasIndex("CurrencyId")
                        .HasDatabaseName("ix_currency_rates_dailies_currency_id");

                    b.HasIndex("ProviderId")
                        .HasDatabaseName("ix_currency_rates_dailies_provider_id");

                    b.ToTable("currency_rates_dailies");
                });

            modelBuilder.Entity("CurrencyRates.Loader.DAL.Model.CurrencyRatesWeekly", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CurrencyBaseId")
                        .HasColumnType("integer")
                        .HasColumnName("currency_base_id");

                    b.Property<DateTime>("CurrencyDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("currency_date");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("currency_id");

                    b.Property<decimal>("MaxRatesPerWeek")
                        .HasColumnType("numeric")
                        .HasColumnName("max_rates_per_week");

                    b.Property<decimal>("MinRatesPerWeek")
                        .HasColumnType("numeric")
                        .HasColumnName("min_rates_per_week");

                    b.Property<int>("ProviderId")
                        .HasColumnType("integer")
                        .HasColumnName("provider_id");

                    b.HasKey("Id")
                        .HasName("pk_currency_rates_weeklies");

                    b.HasIndex("CurrencyBaseId")
                        .HasDatabaseName("ix_currency_rates_weeklies_currency_base_id");

                    b.HasIndex("CurrencyId")
                        .HasDatabaseName("ix_currency_rates_weeklies_currency_id");

                    b.HasIndex("ProviderId")
                        .HasDatabaseName("ix_currency_rates_weeklies_provider_id");

                    b.ToTable("currency_rates_weeklies");
                });

            modelBuilder.Entity("CurrencyRates.Loader.DAL.Model.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ProviderName")
                        .HasColumnType("text")
                        .HasColumnName("provider_name");

                    b.HasKey("Id")
                        .HasName("pk_providers");

                    b.HasIndex("ProviderName")
                        .IsUnique()
                        .HasDatabaseName("ix_providers_provider_name");

                    b.ToTable("providers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Provides currency rates based on Czech crown",
                            ProviderName = "Czech Bank"
                        });
                });

            modelBuilder.Entity("CurrencyRates.Loader.DAL.Model.CurrencyRatesDaily", b =>
                {
                    b.HasOne("CurrencyRates.Loader.DAL.Model.Currency", "CurrencyBase")
                        .WithMany("BaseRatesDaily")
                        .HasForeignKey("CurrencyBaseId")
                        .HasConstraintName("fk_currency_rates_dailies_currencies_currency_base_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CurrencyRates.Loader.DAL.Model.Currency", "Currency")
                        .WithMany("RatesDaily")
                        .HasForeignKey("CurrencyId")
                        .HasConstraintName("fk_currency_rates_dailies_currencies_currency_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CurrencyRates.Loader.DAL.Model.Provider", "Provider")
                        .WithMany("RatesDaily")
                        .HasForeignKey("ProviderId")
                        .HasConstraintName("fk_currency_rates_dailies_providers_provider_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("CurrencyBase");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("CurrencyRates.Loader.DAL.Model.CurrencyRatesWeekly", b =>
                {
                    b.HasOne("CurrencyRates.Loader.DAL.Model.Currency", "CurrencyBase")
                        .WithMany("BaseRatesWeekly")
                        .HasForeignKey("CurrencyBaseId")
                        .HasConstraintName("fk_currency_rates_weeklies_currencies_currency_base_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CurrencyRates.Loader.DAL.Model.Currency", "Currency")
                        .WithMany("RatesWeekly")
                        .HasForeignKey("CurrencyId")
                        .HasConstraintName("fk_currency_rates_weeklies_currencies_currency_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CurrencyRates.Loader.DAL.Model.Provider", "Provider")
                        .WithMany("RatesWeekly")
                        .HasForeignKey("ProviderId")
                        .HasConstraintName("fk_currency_rates_weeklies_providers_provider_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("CurrencyBase");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("CurrencyRates.Loader.DAL.Model.Currency", b =>
                {
                    b.Navigation("BaseRatesDaily");

                    b.Navigation("BaseRatesWeekly");

                    b.Navigation("RatesDaily");

                    b.Navigation("RatesWeekly");
                });

            modelBuilder.Entity("CurrencyRates.Loader.DAL.Model.Provider", b =>
                {
                    b.Navigation("RatesDaily");

                    b.Navigation("RatesWeekly");
                });
#pragma warning restore 612, 618
        }
    }
}
