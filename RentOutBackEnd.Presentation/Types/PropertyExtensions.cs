using RentOutBackEnd.Domain.Entities;
using RentOutBackEnd.Presentation.Dataloaders;

namespace RentOutBackEnd.Presentation.Types;

[Node]
[ExtendObjectType<PropertyPost>]
public class PropertyExtensions
{
    public async Task<PropertyExtras> GetPropertyExtras([Parent] PropertyPost propertyPost, PropertyExtrasDataloader loader)
    {
        return await loader.LoadAsync(propertyPost.Id);
    }
}