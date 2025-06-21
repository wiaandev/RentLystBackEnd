using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using RentroBackEnd.Domain;
using RentroBackEnd.Domain.Entities;

namespace RentroBackEnd.Presentation.Queries;

[QueryType]
public class PropertyQueries
{
    [UsePaging]
    [Authorize(Roles =new[] {"Admin"})]
    public async Task<IList<PropertyPost>> GetProperties(AppDbContext appDbContext, CancellationToken ct)
    {
        return await appDbContext.PropertyPosts.ToListAsync(ct);
    }
    
    [Authorize(Roles =new[] {"User"})]
    public async Task<PropertyPost> GetPropertiesByUserId(AppDbContext appDbContext, [ID] int propertyId)
    {
        var property = await appDbContext.PropertyPosts
            .Where(p => p.Id == propertyId)
            .FirstOrDefaultAsync() ?? throw new Exception("Property does not exist");

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