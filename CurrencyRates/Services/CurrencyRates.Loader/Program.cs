using Autofac.Extensions.DependencyInjection;
using CurrencyRates.Core;
using CurrencyRates.Core.Models;
using CurrencyRates.Loader.DAL;
using CurrencyRates.Loader.Services;
using CurrencyRates.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Threading.Tasks;

namespace CurrencyRates.Loader
{
    internal class Program
    {
        private static IConfiguration _configuration;
        private static ServiceHost _host = new ServiceHost(null);

        static async Task Main(string[] args)
        {
            _host = new ServiceHost(args);
            HostConfiguration();
            await _host.RunAsync((serviceProvider) => { });
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            HostConfiguration();
            return _host.HostBuilder;
        }

        private static void HostConfiguration()
        {
            _host.ConfigureServices((builderContext, services) =>
                {
                    _configuration = builderContext.Configuration;
                    
                    // services.AddHostedService<MigrationInitService>();
                    services.Configure<RabbitSettings>(_configuration.GetSection("RabbitSettings"));
                    services.AddHostedService<RabbitCommandHandlerService>();
                    services.AddMediatR(Assembly.GetExecutingAssembly());

                    // Configure serilog. to create the serilog logger, based on the configuration provided in appsettings.json
                    // provides a fluent interface for building a logging pipeline
                    services.AddSerilogLogging(_configuration);

                    var connection = _configuration.GetConnectionString("LoaderDbConnection");

                    services.AddDbContext<LoaderContext>(options => options
                        .UseNpgsql(connection, x => x.MigrationsHistoryTable("_migrations_history", "loader"))
                        .UseSnakeCaseNamingConvention());

                    services.AddHostedService<MigrationInitService>();


                },
                (services) =>
                {
                    return new AutofacServiceProviderFactory((container) =>
                    {
                        container.Populate(services);
                    });
                });

        }

    }
}
