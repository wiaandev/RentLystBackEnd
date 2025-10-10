using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Entities;

namespace RentlystBackEnd.Presentation.Dataloaders;

public static class PropertyDataLoaders
{

    [DataLoader]
    public static async Task<ILookup<int, PropertyPost>> GetPropertyPostsByUserIdAsync(
        IReadOnlyList<int> userIds,
        AppDbContext appDbContext,
        CancellationToken cancellationToken)
    {
        var posts = await appDbContext.PropertyPosts
            .Include(u => u.Address)
            .Where(pp => userIds.Contains(pp.SellerId))
            .ToListAsync(cancellationToken);

        return posts.ToLookup(p => p.SellerId);
    }

    [DataLoader]
    public static async Task<Dictionary<int, Address>> GetAddressByPropertyId(
        IReadOnlyList<int> propertyIds,
        AppDbContext context,
        CancellationToken cancellationToken)
        => await context.Addresses
            .Where(a => propertyIds.Contains(a.PropertyPostId))
            .ToDictionaryAsync(a => a.PropertyPostId, cancellationToken);
    
    [DataLoader]
    public static async Task<Dictionary<int, PropertyExtras>> GetExtrasByPropertyId(
        IReadOnlyList<int> propertyIds,
        AppDbContext appDbContext,
        CancellationToken ct)
        => await appDbContext.PropertyExtrasEnumerable
            .Where(pp => propertyIds.Contains(pp.PropertyPostId))
            .ToDictionaryAsync(a => a.PropertyPostId, ct);
    
    [DataLoader]
    public static async Task<Dictionary<int, User>> GetSellersByPropertyIdAsync(
        IReadOnlyList<int> propertyIds,
        AppDbContext db,
        CancellationToken ct)
    {
        return await db.PropertyPosts
            .Where(pp => propertyIds.Contains(pp.Id))
            .Select(pp => new { pp.Id, pp.Seller })
            .ToDictionaryAsync(x => x.Id, x => x.Seller, ct);
    }
}