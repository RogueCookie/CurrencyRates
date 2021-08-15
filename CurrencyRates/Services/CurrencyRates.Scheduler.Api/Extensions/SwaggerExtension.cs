using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CurrencyRates.Scheduler.Api.Extensions
{
    /// <summary>
    /// Swagger set up. Implementation of Swagger settings
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Swagger set up. Implementation of Swagger settings
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CurrencyRates scheduler api",
                    Description = "Api for interaction with scheduler",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Valeriia Vaganova",
                        Email = "dev@valkiki.net",
                        Url = new Uri("https://www.facebook.com/valeriia.vaganova.9/")
                    }
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                c.IncludeXmlComments(xmlCommentFullPath);
            });
        }
    }

    /// <summary>
    /// Add swagger in middleware
    /// </summary>
    public static class SwaggerUiExtension
    {
        /// <summary>
        /// Register swagger UI. Enable middleware to serve generated Swagger as a JSON endpoint
        /// </summary>
        /// <param name="app"></param>
        public static void RegisterSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrencyRates scheduler v1");
                c.DisplayRequestDuration();
            });
        }
    }
}