using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Entities;

namespace RentlystBackEnd.Presentation.Queries;

[QueryType]
public class PropertyQueries
{
    [UsePaging]
    [Authorize]
    public async Task<IList<PropertyPost>> GetProperties(AppDbContext appDbContext, CancellationToken ct)
    {
        return await appDbContext.PropertyPosts.ToListAsync(ct);
    }
    
    [Authorize(Roles = ["Seller"])]
    public async Task<IList<PropertyPost>> GetPropertiesByUserId(AppDbContext appDbContext, [ID] int userId, CancellationToken ct)
    {
        var property = await appDbContext.PropertyPosts.Where(u => u.Seller.Id == userId).ToListAsync(ct);

        return property;
    }

    public async Task<PropertyPost> GetPropertyAsync(AppDbContext appDbContext, [ID] int propertyId)
    {
        var property = await appDbContext.PropertyPosts
            .Where(p => p.Id == propertyId)
            .FirstOrDefaultAsync() ?? throw new Exception("Property does not exist");

        return property;
    }
}