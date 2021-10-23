using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRates.Core
{
    /// <summary>
    /// Initialization DI with getting Configuration on
    /// </summary>
    public class ServiceHost : IDisposable
    {
        private readonly string[] _args;
        private readonly Action<IHostBuilder> _configureHostBuilder;
        private IHost _host;

        public ServiceHost(string[] args, Action<IHostBuilder> configureHostBuilder = null)
        {
            _args = args;
            _configureHostBuilder = configureHostBuilder;
        }

        public IHostBuilder HostBuilder { get; private set;  }

        public ServiceHost ConfigureServices<TContainerBuilder>(
            Action<HostBuilderContext, IServiceCollection> configureServices = null,
            Func<IServiceCollection, IServiceProviderFactory<TContainerBuilder>> configureServiceProvider = null,
            Action<HostBuilderContext, TContainerBuilder> configureContainer = null
        ) where TContainerBuilder : class
        {
            HostBuilder = CreateHostBuilder((context, services) =>
            {
                configureServices?.Invoke(context, services);
            });
            if (configureContainer != null)
            {
                HostBuilder.ConfigureContainer(configureContainer);
            }
            
            return this;
        }

        public async Task RunAsync(Action<IServiceProvider> configure = null)
        {
            _host = HostBuilder.Build();
            configure?.Invoke(_host.Services);

            var isService = !(Debugger.IsAttached || _args.Contains("--console"));
            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule?.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                HostBuilder.UseContentRoot(pathToContentRoot);
            }

            _configureHostBuilder?.Invoke(HostBuilder);
            await _host.RunAsync();
        }

        /// <summary>
        /// Allow to read parameters appsettings.json and the parameters were
        /// overwritten with parameters that are passed to the docker compose file
        /// </summary>
        public IHostBuilder CreateHostBuilder(Action<HostBuilderContext, IServiceCollection> configureServices = null)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;
                    config
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                });
            if (configureServices != null)
                builder.ConfigureServices(configureServices);
            return builder;
        }

        public void Dispose()
        {
            _host?.Dispose();
        }
    }
}
