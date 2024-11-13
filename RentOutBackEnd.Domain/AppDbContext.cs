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
    public DbSet<Address> Addresses => this.Set<Address>();
    public DbSet<PropertyExtras> PropertyExtrasEnumerable => this.Set<PropertyExtras>();
    public DbSet<PropertyImage> PropertyImages => this.Set<PropertyImage>();
    public DbSet<RentDuration> RentDurations => this.Set<RentDuration>();
}