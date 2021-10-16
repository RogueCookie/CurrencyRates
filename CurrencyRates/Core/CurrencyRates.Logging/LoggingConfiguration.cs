using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace CurrencyRates.Logging
{
    public static class LoggingConfiguration
    {
        /// <summary>
        /// Extension for adding logging
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration">Configuration for reading Uri of ELK</param>
        /// <example>"http://elasticsearch:9200"</example>
        public static IServiceCollection AddSerilogLogging(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var serviceName = AppDomain.CurrentDomain.FriendlyName;
            var elasticUri = configuration.GetSection("ElasticSearch").Value;

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(elasticUri)
                .Enrich.WithProperty("Application", serviceName)
                .CreateLogger();

            serviceCollection.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(logger));

            return serviceCollection;
        }
    }
}
