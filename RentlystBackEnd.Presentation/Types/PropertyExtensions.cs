using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Entities;
using RentlystBackEnd.Presentation.Dataloaders;

namespace RentlystBackEnd.Presentation.Types;

[Node]
[ExtendObjectType<PropertyPost>]
public class PropertyExtensions
{
    [NodeResolver]
    public static async Task<PropertyPost?> Get(int id, AppDbContext dbContext)
    {
        return await dbContext.PropertyPosts
            .SingleOrDefaultAsync(o => o.Id == id);
    }

    public async Task<PropertyExtras?> GetPropertyExtras([Parent] PropertyPost propertyPost,
        IExtrasByPropertyIdDataLoader loader)
    {
        // Fetch PropertyExtras using the DataLoader
        var propertyExtras = await loader.LoadAsync(propertyPost.Id);

        // If no PropertyExtras are found, return null (or a new PropertyExtras object if you prefer)
        return propertyExtras ?? null; // Change this to new PropertyExtras() if you need a default object
    }

    public async Task<User?> GetPropertySeller([Parent] PropertyPost propertyPost, ISellersByPropertyIdDataLoader loader)
    {
        return await loader.LoadAsync(propertyPost.Id);
    }

    public async Task<Address?> GetPropertyAddress(
        [Parent] PropertyPost propertyPost,
        IAddressByPropertyIdDataLoader loader)
    {
        var address = await loader.LoadAsync(propertyPost.Id);

        if (address is null)
        {
            throw new GraphQLException($"Missing address for propertyPostId {propertyPost.Id}");
        }

        return address;
    }
}
