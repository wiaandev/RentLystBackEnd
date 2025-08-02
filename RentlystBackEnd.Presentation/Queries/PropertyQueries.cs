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

    public async Task<PropertyPost> GetPropertyByIdAsync(AppDbContext appDbContext, [ID] int propertyId)
    {
        var property = await appDbContext.PropertyPosts
            .Where(p => p.Id == propertyId)
            .FirstOrDefaultAsync() ?? throw new Exception("Property does not exist");

        return property;
    }
}