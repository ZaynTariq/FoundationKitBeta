namespace FoundationKit.Authentication.Persistence.Extensions;

using FoundationKit.Authentication.Persistence.DataAccess.ReadRepositories;
using FoundationKit.Authentication.Persistence.DataAccess.WriteRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFoundationKit<TDBContext>(this IServiceCollection services)
        where TDBContext : DbContext
    {
        services.AddScoped<IReadRepository, ReadRepository>(x =>
        {
            var dbContext = x.GetRequiredService<TDBContext>();
            return new ReadRepository(dbContext);
        });

        services.AddScoped<IWriteRepository, WriteRepository>(x =>
        {
            var dbContext = x.GetRequiredService<TDBContext>();
            return new WriteRepository(dbContext);
        });

        return services;
    }
}
