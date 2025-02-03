using Microsoft.EntityFrameworkCore;
using RentOutBackEnd.Domain;
using RentOutBackEnd.Domain.Entities;

namespace RentOutBackEnd.Presentation.Queries;

[QueryType]
public class PropertyQueries
{
    public IQueryable<PropertyPost> GetProperties(AppDbContext appDbContext)
    {
        var properties = appDbContext.PropertyPosts;
        return properties;
    }

    public async Task<PropertyPost> GetPropertyAsync(AppDbContext appDbContext, [ID] int propertyId)
    {
        var property = await appDbContext.PropertyPosts
            .Where(p => p.Id == propertyId)
            .FirstOrDefaultAsync() ?? throw new Exception("Property does not exist");

        return property;
    }
}