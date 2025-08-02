using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Entities;

namespace RentlystBackEnd.Presentation.Dataloaders;

public static class PropertyAddressDataloader
{
    [DataLoader]
    public static async Task<Dictionary<int, Address>> GetAddressByPropertyId(
        IReadOnlyList<int> propertyIds,
        AppDbContext context,
        CancellationToken cancellationToken)
        => await context.Addresses
            .Where(a => propertyIds.Contains(a.PropertyPostId))
            .ToDictionaryAsync(a => a.PropertyPostId, cancellationToken);
}