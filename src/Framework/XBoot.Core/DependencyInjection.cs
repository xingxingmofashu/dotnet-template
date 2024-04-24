using Microsoft.Extensions.Options;
using SqlSugar;
using XBoot.Composables;
using XBoot.Core.IServices;
using XBoot.Core.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddSqlSugar(this IServiceCollection services)
    {
        services.AddScoped(sp =>
        {
            return new SqlSugarScope(new ConnectionConfig()
            {
                ConnectionString = sp.GetRequiredService<IOptions<XBootOptions>>().Value.ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
    public static void AddCurrentUser<TCurrentUser>(this IServiceCollection services) where TCurrentUser : ICurrentUser
    {
        services.AddScoped(typeof(ICurrentUser), typeof(TCurrentUser));
    }
}

