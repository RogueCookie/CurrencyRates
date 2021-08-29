using System;
using CurrencyRates.Scheduler.Api.Extensions;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;

namespace CurrencyRates.Scheduler.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
           _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.RegisterSwagger();
            var אשש = _configuration.GetConnectionString("SchedulerDbConnection");
            services.AddHangfire(configuration => configuration
                .UsePostgreSqlStorage(_configuration.GetConnectionString("SchedulerDbConnection")));

            services.AddHangfireServer();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IBackgroundJobClient backgroundJobClient)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.RegisterSwaggerUi();

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.ContentType = "text/html";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var exceptionHandlerFeature =
                        context.Features.Get<IExceptionHandlerFeature>();
                    logger.LogError(new EventId(), exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);

                    await context.Response.WriteAsync("Something wrong");
                });
            });

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                IgnoreAntiforgeryToken = true,
                Authorization = new List<IDashboardAuthorizationFilter>() { }
            });
           
            //backgroundJobClient.Enqueue(() => Console.WriteLine("Hello, how are you"));

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
