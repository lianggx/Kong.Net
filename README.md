# Kong.Net
.NET Core Client of the Kong

## How to start

Don't talk anything, Just do it!

### Using Kong.Net

``` C#
Install-Package Kong.Net -Version 0.0.4
Install-Package Kong.Extensions -Version 0.0.4
```

### Add JSON section to appsettings.json

``` C#
"kong": {
    "host": "http://10.23.11.1:8001",
    "upstream": {
      "tags": [ "example", "low-priority" ],
      "name": "Kong.Example",
      "hash_on": "none",
      "healthchecks": {
        "active": {
          "unhealthy": {
            "http_statuses": [ 429, 500, 501, 502, 503, 504, 505 ],
            "tcp_failures": 1,
            "timeouts": 1,
            "http_failures": 1,
            "interval": 5
          },
          "type": "http",
          "http_path": "/kong/healthchecks",
          "timeout": 1,
          "healthy": {
            "successes": 1,
            "interval": 5,
            "http_statuses": [ 200, 302 ]
          },
          "https_verify_certificate": true,
          "concurrency": 1
        },
        "passive": {
          "unhealthy": {
            "http_statuses": [ 429, 500, 501, 502, 503, 504, 505 ]
          },
          "healthy": {
            "http_statuses": [ 200, 302 ]
          },
          "type": "http"
        }
      },
      "hash_on_cookie_path": "/",
      "hash_fallback": "none",
      "slots": 10000
    },
    "target": {
      "tags": [ "example", "low-priority" ],
      "target": "192.168.1.10:5200",
      "weight": 100
    }
  }
```

The JSON section kong.target.target Is Kong.Net Service Registration , The sexion value required!

### KongClient Injection To Service container

``` C#
		// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<KongClient>(fat =>
    {
        var options = new KongClientOptions(HttpClientFactory.Create(), this.Configuration["kong:host"]);
        var client = new KongClient(options);
        return client;
    });
}
```

### Configure Kong Client to Startup

``` C#
public void Configure(IApplicationBuilder app, IHostingEnvironment env, KongClient kongClient)
{
    app.UseKong(Configuration, kongClient);

    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseMvc();
}
```

### Custom Kong Client Host:Port

``` C#
// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IHostingEnvironment env, KongClient kongClient)
{
    UseKong(app, kongClient);

    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseMvc();
}

public void UseKong(IApplicationBuilder app, KongClient kongClient)
{
    var upStream = Configuration.GetSection("kong:upstream").Get<UpStream>();
    var target = Configuration.GetSection("kong:target").Get<TargetInfo>();
    var uri = new Uri(Configuration["server.urls"]);
	// This target is your host:port
    target.Target = uri.Authority;
    app.UseKong(kongClient, upStream, target);
}
```
