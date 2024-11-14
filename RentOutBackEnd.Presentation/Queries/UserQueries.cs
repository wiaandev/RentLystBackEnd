using RentOutBackEnd.Domain;
using RentOutBackEnd.Domain.Entities;

namespace RentOutBackEnd.Presentation.Queries;

[QueryType]
public class UserQueries
{
    public IQueryable<User> GetUsers(AppDbContext appDbContext)
    {
        var users = appDbContext.Users;
        return users;
    }
}