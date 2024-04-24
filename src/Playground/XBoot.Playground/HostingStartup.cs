using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using XBoot.Playground.IService;
using XBoot.Playground.Service;

[assembly: HostingStartup(typeof(XBoot.Playground.HostingStartup))]
namespace XBoot.Playground
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