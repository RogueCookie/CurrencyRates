using CurrencyRates.Loader.DAL.Model;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<Currency>().HasKey("Id");

            modelBuilder.Entity<CurrencyRatesWeekly>().HasKey("Id");
            modelBuilder.Entity<CurrencyRatesWeekly>()
                .HasOne(x => x.Provider)
                .WithMany(x => x.CurrencyRatesWeeklies)
                .HasForeignKey(x => x.ProviderId);
            modelBuilder.Entity<CurrencyRatesWeekly>()
                .HasOne(x => x.Currency)
                .WithMany(x => x.CurrencyRatesWeeklies)
                .HasForeignKey(x => x.CurrencyId);
            //modelBuilder.Entity<CurrencyRatesWeekly>()
            //    .HasOne(x => x.CurrencyBase)
            //    .WithMany(x => x.CurrencyRatesWeeklies)
            //    .HasForeignKey(x => x.CurrencyBaseId);


            modelBuilder.Entity<CurrencyRatesDaily>().HasKey("Id");
            modelBuilder.Entity<CurrencyRatesDaily>()
                .HasOne(x => x.Provider)
                .WithMany(x => x.CurrencyRatesDailies)
                .HasForeignKey(x => x.ProviderId);
            modelBuilder.Entity<CurrencyRatesDaily>()
                .HasOne(x => x.Currency)
                .WithMany(x => x.CurrencyRatesDailies)
                .HasForeignKey(x => x.CurrencyId);
            //modelBuilder.Entity<CurrencyRatesDaily>()
            //    .HasOne(x => x.CurrencyBase)
            //    .WithMany(x => x.CurrencyRatesDailies)
            //    .HasForeignKey(x => x.CurrencyBaseId);

            modelBuilder.Entity<Provider>().HasKey("Id");

            base.OnModelCreating(modelBuilder);
        }
    }
}