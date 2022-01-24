using Funq;
using ServiceStack;
using mywebapp.ServiceInterface;

[assembly: HostingStartup(typeof(mywebapp.AppHost))]

namespace mywebapp;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            services.AddScoped<CustomTraceHolder>();
            // Configure ASP.NET Core IOC Dependencies
        })
        .Configure(app => {
            // Configure ASP.NET Core App
            app.UseMiddleware<TraceIdMiddleware>();

            if (!HasInit)
                app.UseServiceStack(new AppHost());
        });

    public AppHost() : base("mywebapp", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        // Configure ServiceStack only IOC, Config & Plugins
        SetConfig(new HostConfig {
            UseSameSiteCookies = true,
        });
    }
}


