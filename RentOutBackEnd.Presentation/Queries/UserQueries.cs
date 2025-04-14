using System.Security.Claims;
using HotChocolate.Authorization;
using RentOutBackEnd.Domain;
using RentOutBackEnd.Domain.Entities;
using RentOutBackEnd.Domain.Services;

namespace RentOutBackEnd.Presentation.Queries;

[QueryType]
public class UserQueries
{
    public IQueryable<User> GetUsers(AppDbContext appDbContext)
    {
        var users = appDbContext.Users;
        return users;
    }
    
    public Task<User> GetMe([Service] UserService userService, ClaimsPrincipal claimsPrincipal)
    {
        return userService.GetByClaimsPrincipalAsync(claimsPrincipal);
    }
}