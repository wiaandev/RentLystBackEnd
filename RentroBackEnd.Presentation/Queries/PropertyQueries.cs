using Microsoft.EntityFrameworkCore;
using RentroBackEnd.Domain;
using RentroBackEnd.Domain.Entities;

namespace RentroBackEnd.Presentation.Queries;

[QueryType]
public class PropertyQueries
{
    [UsePaging]
    public async Task<IList<PropertyPost>> GetProperties(AppDbContext appDbContext, CancellationToken ct)
    {
        return await appDbContext.PropertyPosts.ToListAsync(ct);
    }

    public async Task<PropertyPost> GetPropertyAsync(AppDbContext appDbContext, [ID] int propertyId)
    {
        var property = await appDbContext.PropertyPosts
            .Where(p => p.Id == propertyId)
            .FirstOrDefaultAsync() ?? throw new Exception("Property does not exist");

        return property;
    }
}