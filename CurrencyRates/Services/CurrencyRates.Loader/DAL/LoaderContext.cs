using CurrencyRates.Core.Enums;
using CurrencyRates.Loader.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CurrencyRates.Loader.DAL
{
    public class LoaderContext : DbContext
    {
        private const string SchemaName = "loader";

        public LoaderContext(DbContextOptions<LoaderContext> options) : base(options)
        {
        }

        public DbSet<CurrencyRatesDaily> CurrencyRatesDailies { get; set; }
        public DbSet<CurrencyRatesWeekly> CurrencyRatesWeeklies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);

            modelBuilder.Entity<Currency>().HasKey(x => x.Id);
            modelBuilder.Entity<Currency>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Currency>().HasIndex(x => x.Alias).IsUnique();
            modelBuilder.Entity<Currency>()
                .Property(x => x.Alias)
                .HasConversion(
                    i => i.ToString(),
                    i => (TypeOfCurrency) Enum.Parse(typeof(TypeOfCurrency), i));


            modelBuilder.Entity<CurrencyRatesWeekly>().HasKey(x => x.Id);
            modelBuilder.Entity<CurrencyRatesWeekly>()
                .HasOne(x => x.Provider)
                .WithMany(x => x.RatesWeekly)
                .HasForeignKey(x => x.ProviderId);
            modelBuilder.Entity<CurrencyRatesWeekly>()
                .HasOne(x => x.Currency)
                .WithMany(x => x.RatesWeekly)
                .HasForeignKey(x => x.CurrencyId);
            modelBuilder.Entity<CurrencyRatesWeekly>()
                .HasOne(x => x.CurrencyBase)
                .WithMany(x => x.BaseRatesWeekly)
                .HasForeignKey(x => x.CurrencyBaseId);


            modelBuilder.Entity<CurrencyRatesDaily>().HasKey(x => x.Id);
            modelBuilder.Entity<CurrencyRatesDaily>()
                .HasOne(x => x.Provider)
                .WithMany(x => x.RatesDaily)
                .HasForeignKey(x => x.ProviderId);
            modelBuilder.Entity<CurrencyRatesDaily>()
                .HasOne(x => x.Currency)
                .WithMany(x => x.RatesDaily)
                .HasForeignKey(x => x.CurrencyId);
            modelBuilder.Entity<CurrencyRatesDaily>()
                .HasOne(x => x.CurrencyBase)
                .WithMany(x => x.BaseRatesDaily)
                .HasForeignKey(x => x.CurrencyBaseId);

            modelBuilder.Entity<Provider>().HasKey(x => x.Id);
            modelBuilder.Entity<Provider>().HasIndex(x => x.ProviderName).IsUnique();
            modelBuilder.Entity<Provider>().HasData(new Provider()
            {
                Id = 1,
                ProviderName = "Czech Bank",
                Description = "Provides currency rates based on Czech crown"
            });

            var currencies = Enum.GetValues(typeof(TypeOfCurrency)).OfType<TypeOfCurrency>().Select(x => new Currency()
            {
                Alias = x,
                Id = (int) x
            });

            modelBuilder.Entity<Currency>().HasData(currencies);

            base.OnModelCreating(modelBuilder);
        }
    }
}