using System;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Loader.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Loader.Services
{
    /// <summary>
    /// At the first start, rolls migrations to the database if it is not exist 
    /// </summary>
    public class MigrationInitService : IHostedService
    {
        private readonly LoaderContext _context;
        private readonly ILogger<MigrationInitService> _logger;

        public MigrationInitService(LoaderContext context, ILogger<MigrationInitService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Begin database initialization and migration");

            await _context.Database.EnsureCreatedAsync(cancellationToken);
            var migrations = _context.Database.GetMigrations();
            _logger.LogInformation($"Migrations {string.Join(",", migrations)}");

            var appliedMigrations = await _context.Database.GetAppliedMigrationsAsync(cancellationToken);
            _logger.LogInformation($"Applied migrations {string.Join(",", appliedMigrations)}");

            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync(cancellationToken);
            _logger.LogInformation($"Pending migrations {string.Join(",", pendingMigrations)}");

            try
            {
                await _context.Database.MigrateAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error db migrate");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}