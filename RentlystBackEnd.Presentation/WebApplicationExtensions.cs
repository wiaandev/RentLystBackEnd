using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Options;
using RentlystBackEnd.Domain.Services;

namespace RentlystBackEnd.Presentation;

public static class WebApplicationExtensions
{
    public static async Task Init(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        var dropOptions = scope.ServiceProvider.GetRequiredService<IOptions<DropOptions>>().Value;
        var migrateOptions = scope.ServiceProvider.GetRequiredService<IOptions<MigrateOptions>>().Value;
        var seedOptions = scope.ServiceProvider.GetRequiredService<IOptions<SeedOptions>>().Value;
        var createOptions = scope.ServiceProvider.GetRequiredService<IOptions<CreateOptions>>().Value;
        
        // if (createOptions.Enabled)
        // {
        //
        //     var connectionStringBuilder = new Npgsql.NpgsqlConnectionStringBuilder(app.Configuration.GetConnectionString("RentroDatabase"));
        //     var database = connectionStringBuilder.Database;
        //
        //     // Connect to the default 'postgres' DB to create a new one
        //     connectionStringBuilder.Database = "postgres";
        //
        //     var options = new DbContextOptionsBuilder<DbContext>()
        //         .UseNpgsql(connectionStringBuilder.ConnectionString, b => b.CommandTimeout(600))
        //         .Options;
        //
        //     var dbContext = new DbContext(options);
        //
        //     var createDbSql = $"CREATE DATABASE RentroDatabase";
        //
        //     // Execute the creation
        //     await dbContext.Database.ExecuteSqlRawAsync(createDbSql);
        // }



        if (dropOptions.Enabled)
        {
            Console.WriteLine("Dropping database");
            await using var dbContext = await factory.CreateDbContextAsync();
            await dbContext.Database.EnsureDeletedAsync();
        }

        
        if (migrateOptions.Enabled)
        {
            Console.WriteLine("Migrating database");
            await using var dbContext = await factory.CreateDbContextAsync();
            await dbContext.Database.MigrateAsync();
        }

        if (seedOptions.Enabled)
        {
            Console.WriteLine("Seeding Db");
            await using var dbContext = await factory.CreateDbContextAsync();
            var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();

            await seedService.Seed();
        }
    }
}
    