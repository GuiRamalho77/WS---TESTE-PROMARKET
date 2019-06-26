using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SampleWebApiAspNetCore.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace WebApplication11
{
    public class Startup
    {
        private static AppSettings _appSettings;
        public Startup(IConfiguration configuration)
        {
            ConfigureServices(new ServiceCollection());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddSwaggerGen(c =>
            {   
                c.SwaggerDoc($"v1", new Info
                {
                    Title = $"ProMarke WS",
                    Version = $"v1",
                    Description = $"Teste ProMarket"
                });
            });
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile($"appsettings.json", optional: false);

            var configuration = builder.Build();
            _appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddOptions<AppSettings>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseMvc();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Proxy Api");
            });

        }
        private string ReturnAppSettingsFileName() => $"appsettings.json";
    }
}
