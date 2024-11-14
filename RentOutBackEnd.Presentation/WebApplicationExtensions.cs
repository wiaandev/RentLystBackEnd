using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentOutBackEnd.Domain;
using RentOutBackEnd.Domain.Options;
using RentOutBackEnd.Domain.Services;

namespace RentOutBackEnd.Presentation;

public static class WebApplicationExtensions
{
    public static async Task Init(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        var seedOptions = scope.ServiceProvider.GetRequiredService<IOptions<SeedOptions>>().Value;

        if (seedOptions.Enabled)
        {
            Console.WriteLine("Seeding Db");
            await using var dbContext = await factory.CreateDbContextAsync();
            var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();

            await seedService.Seed();
        }
    }
}