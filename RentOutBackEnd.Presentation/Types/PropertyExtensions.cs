using RentOutBackEnd.Domain.Entities;
using RentOutBackEnd.Presentation.Dataloaders;

namespace RentOutBackEnd.Presentation.Types;

[Node]
[ExtendObjectType<PropertyPost>]
public class PropertyExtensions
{
    public async Task<PropertyExtras?> GetPropertyExtras([Parent] PropertyPost propertyPost, PropertyExtrasDataloader loader)
    {
        // Fetch PropertyExtras using the DataLoader
        var propertyExtras = await loader.LoadAsync(propertyPost.Id);

        // If no PropertyExtras are found, return null (or a new PropertyExtras object if you prefer)
        return propertyExtras ?? null;  // Change this to new PropertyExtras() if you need a default object
    }
}