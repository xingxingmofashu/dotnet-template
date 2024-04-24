using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using XBoot.Playground.API;
using XBoot.Playground.API.Service;
using XBoot.Playground.Services;

[assembly: HostingStartup(typeof(HostingStartup))]
namespace XBoot.Playground.API
{

    public class HostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddScoped<IDemoService, DemoService>();

                services.AddHttpClient();
            });
        }
    }
}