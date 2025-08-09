using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Entities;

namespace RentlystBackEnd.Presentation.Types;

[Node]
[ExtendObjectType(typeof(Address))]
public static class AddressExtensions
{

    [NodeResolver]
    public static async Task<Address?> Get([ID] int id, AppDbContext dbContext)
    {
        return await dbContext.Addresses.SingleOrDefaultAsync(o => o.Id == id);
    }
    
    
    [NodeResolver]
    public static string GetFullAddress([Parent] Address address)
    {
        var street = address.StreetName ?? string.Empty;
        var number = address.StreetNumber ?? string.Empty;
        var suburb = address.Suburb ?? string.Empty;

        return $"{street} {number}, {suburb}".Trim();
    }
}