using Microsoft.EntityFrameworkCore;
using RentOutBackEnd.Domain;
using RentOutBackEnd.Domain.Entities;

namespace RentOutBackEnd.Presentation.Dataloaders;

public class PropertyAddressDataloader: BatchDataLoader<int, Address>
{
    // TODO: Review data loader
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
    
    public PropertyAddressDataloader(IDbContextFactory<AppDbContext> dbContextFactory, IBatchScheduler _batchScheduler,
        DataLoaderOptions? options = null) : base(_batchScheduler, options)
    {
        this._dbContextFactory = dbContextFactory;
    }
    
    [DataLoader]
    protected override async Task<IReadOnlyDictionary<int, Address>> LoadBatchAsync(IReadOnlyList<int> keys,
        CancellationToken ct)
    {
        await using var dbContext = this._dbContextFactory.CreateDbContext();

        var propertyPosts = await dbContext.Addresses.Where(pp => keys.Contains(pp.Id)).ToListAsync(ct);

        return propertyPosts.ToDictionary(pa => pa.Id);
    }
}