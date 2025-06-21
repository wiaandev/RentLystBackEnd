using Microsoft.EntityFrameworkCore;
using RentOutBackEnd.Domain;
using RentOutBackEnd.Domain.Entities;

namespace RentOutBackEnd.Presentation.Dataloaders;

public class PropertyPostDataloader: BatchDataLoader<int, PropertyPost>
{
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;
    
    public PropertyPostDataloader(IDbContextFactory<AppDbContext> dbContextFactory, IBatchScheduler _batchScheduler,
        DataLoaderOptions? options = null) : base(_batchScheduler, options)
    {
        this.dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, PropertyPost>> LoadBatchAsync(IReadOnlyList<int> keys,
        CancellationToken ct)
    {
        await using var dbContext = await this.dbContextFactory.CreateDbContextAsync(ct);

        var propertyPosts = await dbContext.PropertyPosts.Where(pp => keys.Contains(pp.Id)).ToListAsync(ct);

        return propertyPosts.ToDictionary(pp => pp.Id);
    }
}