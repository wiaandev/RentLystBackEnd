using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Entities;
using RentlystBackEnd.Domain.Options;
using RentlystBackEnd.Domain.Services;

namespace RentlystBackEnd.Presentation;

public static class IServiceCollectionExtensions
{
    
    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DropOptions>(options =>
            configuration.GetSection(DropOptions.Key).Bind(options));
        services.Configure<MigrateOptions>(options =>
            configuration.GetSection(MigrateOptions.Key).Bind(options));
        services.Configure<SeedOptions>(options =>
            configuration.GetSection(SeedOptions.Key).Bind(options));
        services.Configure<CreateOptions>(options =>
            configuration.GetSection(CreateOptions.Key).Bind(options));
        // services.AddHttpClient<GeocodingService>(client =>
        // {
        //     client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/");
        // });
        return services;
    }
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPooledDbContextFactory<AppDbContext>(
            options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("RentroDatabase"),
                    b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery).MigrationsAssembly("RentlystBackEnd.Presentation")
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
    
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ClaimsPrincipal>(s => s.GetService<IHttpContextAccessor>()!.HttpContext!.User);
        services.AddScoped<SeedService>();

        return services;
    }
    
    public static void AddAuth(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddRoles<Role>()
            // .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()
            .AddDefaultTokenProviders();

        services.AddAuthorizationBuilder();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opts =>
        {
            opts.ExpireTimeSpan = new TimeSpan(1, 0, 0, 0);
            opts.LoginPath = "/login";
        });
    }
}