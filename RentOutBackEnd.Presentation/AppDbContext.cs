using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentOutBackEnd.Domain.Entities;

namespace RentOutBackEnd.Presentation;

public class AppDbContext: IdentityDbContext<User, Role, int>
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<PropertyPost> PropertyPosts => this.Set<PropertyPost>();
}