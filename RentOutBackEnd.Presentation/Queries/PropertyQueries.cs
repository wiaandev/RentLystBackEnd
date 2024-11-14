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
}