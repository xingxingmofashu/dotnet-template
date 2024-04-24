using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(XBoot.Core.HostingStartup))]
namespace XBoot.Core
{
    public class HostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((WebHostBuilderContext context, IServiceCollection services) =>
            {

            });
        }
    }
}