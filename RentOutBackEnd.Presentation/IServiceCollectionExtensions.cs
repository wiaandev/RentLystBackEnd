using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentOutBackEnd.Presentation;

namespace RentOutBackEnd.Domain;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPooledDbContextFactory<AppDbContext>(
            options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("RentOutDatabase"),
                    b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                        .CommandTimeout(600));
                        // .UseNetTopologySuite()
                        // .UseHierarchyId());
                options.EnableDetailedErrors();
                // TODO: only dev or testing
                // options.EnableSensitiveDataLogging();
            });

        // For Legacy services
        services.AddDbContext<AppDbContext>();

        return services;
    }
}