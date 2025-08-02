using HotChocolate.Authorization;
using RentlystBackEnd.Domain.Entities;
using RentlystBackEnd.Presentation.Dataloaders;

namespace RentlystBackEnd.Presentation.Types;

[Node]
[ExtendObjectType<User>]
public class UserExtensions
{
    [Authorize(Roles = ["Seller"])]
    public async Task<IList<PropertyPost>> GetPropertyPostsAsync(
        [Parent] User user,
        IPropertyPostsByUserIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var posts = await dataLoader.LoadAsync(user.Id, cancellationToken);
        return posts ?? [];
    }
}
    
