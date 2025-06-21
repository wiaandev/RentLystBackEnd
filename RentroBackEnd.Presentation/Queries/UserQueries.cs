using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RentroBackEnd.Domain;
using RentroBackEnd.Domain.Entities;

namespace RentroBackEnd.Presentation.Queries;

[QueryType]
public class UserQueries
{
    public IQueryable<User> GetUsers(AppDbContext appDbContext)
    {
        var users = appDbContext.Users;
        return users;
    }

    public async Task<User?> GetMe(AppDbContext appDbContext, ClaimsPrincipal claims)
    {
        var partyClaim = claims.FindFirst((c) =>
            c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        if (partyClaim is null)
        {
            return null;
        }

        var user = await appDbContext.Users.FirstAsync(user => user.Id == int.Parse(partyClaim.Value));

        return user;
    }
}
    