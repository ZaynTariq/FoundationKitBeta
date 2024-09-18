namespace FoundationKit.Authentication.Persistence.Extensions;

using FoundationKit.Authentication.Persistence.DataAccess.ReadRepositories;
using FoundationKit.Authentication.Persistence.DataAccess.WriteRepositories;
using FoundationKit.Authentication.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFoundationKit<TDBContext>(this IServiceCollection services, Action<EFConfiguration> configurationCallback = default!)
        where TDBContext : DbContext
    {

        services.AddScoped<IReadRepository, ReadRepository>(x =>
        {
            var dbContext = x.GetRequiredService<TDBContext>();

            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            var configuration = EFConfiguration.Create();

            if (configurationCallback is not null)
            {
                configurationCallback(configuration);
            }

            return new ReadRepository(dbContext, configuration);
        });

        services.AddScoped<IWriteRepository, WriteRepository>(x =>
        {
            var dbContext = x.GetRequiredService<TDBContext>();
            return new WriteRepository(dbContext);
        });

        return services;
    }

    public static IServiceCollection AddFoundationKit(this IServiceCollection services, Func<IServiceProvider, DbContext> dbContextCallback, Action<EFConfiguration> configurationCallback = default!)

    {
        services.AddScoped<IReadRepository, ReadRepository>(x =>
        {
            var dbContext = dbContextCallback(x);

            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            var configuration = EFConfiguration.Create();

            if (configurationCallback is not null)
            {
                configurationCallback(configuration);
            }

            return new ReadRepository(dbContext, configuration);
        });

        services.AddScoped<IWriteRepository, WriteRepository>(x =>
        {
            var dbContext = dbContextCallback(x); ;
            return new WriteRepository(dbContext);
        });

        return services;
    }
}
