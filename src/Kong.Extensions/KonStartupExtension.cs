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
        public static IServiceCollection AddKong(this IServiceCollection service, KongClientOptions options)
        {
            KongClient client = new KongClient(options);
            service.AddSingleton<KongClient>(fat => client);
            return service;
        }

        /// <summary>
        /// 
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
            return app.UseKong(client, upStream, target);
        }

        public static IApplicationBuilder UseKong(this IApplicationBuilder app, KongClient client, UpStream upStream, TargetInfo target)
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

            app.UseKongHealthChecks(upStream);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0} UpStream registration completed!", upStream.Name);
            Console.WriteLine("The target is {0}", target.Target);
            Console.ForegroundColor = ConsoleColor.Gray;

            return app;
        }

        private static IApplicationBuilder UseKongHealthChecks(this IApplicationBuilder app, UpStream upStream)
        {
            app.Map(upStream.HealthChecks.Active.Http_path, s =>
            {
                s.Run(async context =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Healthchecks at: {0}", DateTime.Now);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    await context.Response.WriteAsync("ok");
                });
            });

            return app;
        }
    }
}
