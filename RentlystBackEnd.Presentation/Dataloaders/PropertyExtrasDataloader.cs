using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Entities;

namespace RentOutBackEnd.Presentation.Dataloaders;

public class PropertyExtrasDataloader : BatchDataLoader<int, PropertyExtras>
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public PropertyExtrasDataloader(IDbContextFactory<AppDbContext> dbContextFactory, IBatchScheduler _batchScheduler,
        DataLoaderOptions? options = null) : base(_batchScheduler, options)
    {
        this._dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, PropertyExtras>> LoadBatchAsync(IReadOnlyList<int> keys,
        CancellationToken ct)
    {
        await using var dbContext = this._dbContextFactory.CreateDbContext();

        var propertyPosts = await dbContext.PropertyExtrasEnumerable.Where(pp => keys.Contains(pp.Id)).ToListAsync(ct);

        return propertyPosts.ToDictionary(pp => pp.PropertyPostId);
    }
}