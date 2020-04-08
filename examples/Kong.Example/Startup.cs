using Kong.Extensions;
using Kong.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Kong.Example
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
            services.AddKong(() =>
            {
                var options = new KongClientOptions(HttpClientFactory.Create(), this.Configuration["kong:host"]);
                return options;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, KongClient kongClient)
        {
            UseKong(app, kongClient);

            if (env.EnvironmentName == Microsoft.Extensions.Hosting.Environments.Development)
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting()
                  .UseEndpoints(endpoints =>
                  {
                      endpoints.MapDefaultControllerRoute();
                  });
        }

        public void UseKong(IApplicationBuilder app, KongClient kongClient)
        {
            var upStream = Configuration.GetSection("kong:upstream").Get<UpStream>();
            var target = Configuration.GetSection("kong:target").Get<TargetInfo>();
            var uri = new Uri(Configuration["server.urls"]);
            target.Target = uri.Authority;
            app.UseKong(kongClient, upStream, target, OnExecuter);
        }

        /// <summary>
        /// Custom HealthChecks
        /// </summary>
        /// <param name="context"></param>
        public void OnExecuter(HttpContext context)
        {
            context.Response.StatusCode = 200;
        }
    }
}
