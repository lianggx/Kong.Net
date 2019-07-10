using Kong.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kong.Extensions
{
    public static class KongStartupExtensions
    {

        /// <summary>
        /// Adds Kong Client to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddKong(this IServiceCollection service, Func<KongClientOptions> options)
        {
            return service.AddKong(options.Invoke());
        }

        /// <summary>
        /// Adds Kong Client to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddKong(this IServiceCollection service, KongClientOptions options)
        {
            KongClient client = new KongClient(options);
            service.AddSingleton<KongClient>(fat => client);
            return service;
        }

        /// <summary>
        ///  Adds Kong Client to the Kong Gateway UpStreams And HealthChecks Registration
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <param name="client"></param>
        /// <param name="targetHost">upstrea.target.target{target host:port}</param>
        /// <returns></returns>
        public static IApplicationBuilder UseKong(this IApplicationBuilder app, IConfiguration configuration, KongClient client)
        {
            var upStream = configuration.GetSection("kong:upstream").Get<UpStream>();
            var target = configuration.GetSection("kong:target").Get<TargetInfo>();
            return app.UseKong(client, upStream, target, null);
        }

        /// <summary>
        /// Adds Kong Client to the Kong Gateway UpStreams And HealthChecks Registration
        /// </summary>
        /// <param name="app"></param>
        /// <param name="client"></param>
        /// <param name="upStream"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseKong(this IApplicationBuilder app, KongClient client, UpStream upStream, TargetInfo target)
        {
            return app.UseKong(client, upStream, target, null);
        }

        /// <summary>
        /// Adds Kong Client to the Kong Gateway UpStreams And HealthChecks Registration
        /// </summary>
        /// <param name="app"></param>
        /// <param name="client"></param>
        /// <param name="upStream"></param>
        /// <param name="target"></param>
        /// <param name="onExecuter">The parameter allow you custom healthchecks response</param>
        /// <returns></returns>
        public static IApplicationBuilder UseKong(this IApplicationBuilder app, KongClient client, UpStream upStream, TargetInfo target, Action<HttpContext> onExecuter)
        {
            if (upStream == null)
                throw new ArgumentNullException(nameof(upStream));
            if (target == null || string.IsNullOrEmpty(target.Target))
                throw new ArgumentNullException(nameof(target));

            upStream.Id = Guid.NewGuid();
            upStream.Created_at = DateTime.Now;
            upStream = client.UpStream.UpdateOrCreate(upStream).GetAwaiter().GetResult();
            target.Id = Guid.NewGuid();
            target.Created_at = DateTime.Now;
            target.UpStream = new TargetInfo.UpStreamId { Id = upStream.Id.Value };
            target = client.Target.Add(target).GetAwaiter().GetResult();

            app.UseKongHealthChecks(upStream, onExecuter);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0} UpStream registration completed!", upStream.Name);
            Console.WriteLine("The target is {0}", target.Target);
            Console.ForegroundColor = ConsoleColor.Gray;

            return app;
        }

        private static IApplicationBuilder UseKongHealthChecks(this IApplicationBuilder app, UpStream upStream, Action<HttpContext> onExecuter)
        {
            app.Map(upStream.HealthChecks.Active.Http_path, s =>
            {
                s.Run(async context =>
                {
                    if (onExecuter != null)
                    {
                        onExecuter(context);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Healthchecks at: {0}", DateTime.Now);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        await context.Response.WriteAsync("ok");
                    }
                });
            });

            return app;
        }
    }
}
