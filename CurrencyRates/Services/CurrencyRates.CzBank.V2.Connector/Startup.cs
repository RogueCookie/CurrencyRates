using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CurrencyRates.CzBank.V2.Connector.Interfaces;
using CurrencyRates.CzBank.V2.Connector.Services;
using CurrencyRates.Logging;

namespace CurrencyRates.CzBank.V2.Connector
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient(Constants.GeneralConstants.BaseCzBankUri, client =>
            {
                client.BaseAddress = new Uri("https://www.cnb.cz");
            }).AddPolicyHandler(GetRetryPolicy());

            services.AddSerilogLogging(Configuration);

            services.AddHostedService<JobRegistrationService>();
            services.AddTransient<IClientRatesConnectorService, ClientRatesRatesConnectorService>();
            services.AddTransient<IDataCommandSender, DataCommandSender>();
            services.AddHostedService<RabbitCommandHandlerService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CurrencyRates.CzBank.V2.Connector", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrencyRates.CzBank.V2.Connector v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Add policy for retry HTTP requests (for HttpClientFactory resilience and transient-fault handling capabilities).
        /// Handle HttpRequestExceptions, 404 not found and 5xx status codes.
        /// </summary>
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
