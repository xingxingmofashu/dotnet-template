using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SqlSugar;
using XBoot.Core.IServices;
using XBoot.Core.Services;

[assembly: HostingStartup(typeof(XBoot.Core.HostingStartup))]
namespace XBoot.Core
{
    public class HostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((WebHostBuilderContext context, IServiceCollection services) =>
            {
                services.AddScoped(sp =>
                {
                    return new SqlSugarScope(sp.GetRequiredService<IOptions<XBootConnectionConfig>>().Value);
                });

                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            });
        }
    }
}