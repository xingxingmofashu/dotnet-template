[assembly: HostingStartup(typeof(XBoot.WebHost.HostingStartup))]
namespace XBoot.WebHost;

public class HostingStartup : IHostingStartup
{
    public void Configure(IWebHostBuilder builder)
    {
        builder.ConfigureServices((WebHostBuilderContext context, IServiceCollection services) =>
        {
           
        });
    }
}

