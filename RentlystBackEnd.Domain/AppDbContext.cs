using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain.Entities;

namespace RentlystBackEnd.Domain;

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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PropertyExtras>()
            .HasOne(pe => pe.Property)
            .WithMany() // Assuming PropertyPost does not have a collection of PropertyExtras
            .HasForeignKey(pe => pe.PropertyPostId)
            .IsRequired(); // Ensures PropertyPostId is required and links correctly

        base.OnModelCreating(modelBuilder);
    }
}