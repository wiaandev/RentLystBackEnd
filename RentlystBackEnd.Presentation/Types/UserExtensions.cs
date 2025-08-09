using System.Security.Claims;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Entities;
using RentlystBackEnd.Presentation.Dataloaders;

namespace RentlystBackEnd.Presentation.Types;

[Node]
[ExtendObjectType<User>]
public class UserExtensions
{
    [NodeResolver]
    public static async Task<User?> Get(int id, AppDbContext dbContext)
    {
        return await dbContext.Users
            .SingleOrDefaultAsync(o => o.Id == id);
    }

    [Authorize(Roles = ["Seller"])]
    public async Task<IList<PropertyPost>> GetPropertyPostsAsync(
        [Parent] User user,
        IPropertyPostsByUserIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var posts = await dataLoader.LoadAsync(user.Id, cancellationToken);
        return posts ?? [];
    }
    
    public async Task<IList<string>> GetRolesUserNotMeAsync(
        [Parent] Me me,
        UserManager<User> userManager)
    {
        if (me.User == null)
            return Array.Empty<string>();

        return await userManager.GetRolesAsync(me.User);
    }

}
    
