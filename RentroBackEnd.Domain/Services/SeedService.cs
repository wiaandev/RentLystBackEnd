using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentroBackEnd.Domain.Entities;

namespace RentroBackEnd.Domain.Services;

public class SeedService(AppDbContext appDbContext, UserManager<User> userManager)
{
    public async Task Seed()
    {
        await this.SeedUsers();
        await this.SeedPropertyPosts();
        await this.SeedPropertyAddresses();
        await this.SeedPropertyExtras();
    }

    public async Task SeedUsers()
    {
        var users = new List<(User user, string password)>
        {
            (
                new User
                {
                    UserName = "wiaan@stackworx.io",
                    Email = "wiaan@stackworx.io",
                    FirstName = "Wiaan",
                    LastName = "Duvenhage",
                    IsRenter = true,
                }, "SecurePassword123!"
            ),
            (
                new User
                {
                    UserName = "charleneduvenhage@gmail.com",
                    Email = "charleneduvenhage@gmail.com",
                    FirstName = "Charlene",
                    LastName = "Duvenhage",
                    IsRenter = false,
                }, "AnotherSecurePassword123!"
            )
        };

        foreach (var (user, password) in users)
        {
            var createUser = await userManager.CreateAsync(user, password);
            if (!createUser.Succeeded)
            {
                var errors = string.Join("; ", createUser.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user '{user.Email}': {errors}");
            }

            var result = await userManager.RemoveClaimAsync(user, new Claim("All_Admin", "All"));
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to add claim to user '{user.Email}': {errors}");
            }
        }
        
        await userManager.AddToRoleAsync(users[0].user, "User");
        await userManager.AddToRoleAsync(users[1].user, "Admin");
    }

    public async Task SeedPropertyPosts()
    {
        var properties = new List<PropertyPost>
        {
            new()
            {
                PropertyType = PropertyPost.RentType.Apartment,
                WeeklyAmount = 600,
                BedroomAmount = 2,
                BathroomAmount = 1,
                ParkingAmount = 2,
                PetAmount = 2,
                PetType = new List<PropertyPost.AllowedPetType>
                {
                    PropertyPost.AllowedPetType.Bird,
                    PropertyPost.AllowedPetType.Dog
                },
                CreatedAt = DateTime.UtcNow.AddHours(2),
            }
        };

        appDbContext.PropertyPosts.AddRange(properties);
        await appDbContext.SaveChangesAsync(); // Save to persist the data and generate IDs
    }
    
    public async Task SeedPropertyAddresses()
    {
        var addresses = new List<Address>
        {
            new()
            {
                PropertyPostId = 1,
                StreetName = "Main Street",
                StreetNumber = "23",
                Suburb = "Lakeview",
                City = "Welkom",
                Province = "Free State"
            }
        };

        appDbContext.AddRange(addresses);
        await appDbContext.SaveChangesAsync(); // Save to persist the data and generate IDs
    }

    public async Task SeedPropertyExtras()
    {
        // Retrieve the PropertyPost to get its generated ID
        var propertyPost = await appDbContext.PropertyPosts.ToListAsync();

        if (!propertyPost.Any())
        {
            throw new Exception("No propertyPost found.");
        }

        var propertyExtras = new List<PropertyExtras>
        {
            new()
            {
                Property = new PropertyPost
                {
                    PropertyType = PropertyPost.RentType.Apartment,
                    WeeklyAmount = 600,
                    BedroomAmount = 2,
                    BathroomAmount = 1,
                    ParkingAmount = 2,
                    PetAmount = 2,
                    PetType = new List<PropertyPost.AllowedPetType>
                    {
                        PropertyPost.AllowedPetType.Bird,
                        PropertyPost.AllowedPetType.Dog
                    },
                    CreatedAt = DateTime.UtcNow.AddHours(2)
                },
                PropertyPostId = 1, // Use the actual ID of the saved PropertyPost
                HasFiber = true,
                PetsAllowed = true,
                HasPool = true,
                HasGarden = false,
                HasPatio = true,
                HasFlatlet = false
            }
        };

        appDbContext.PropertyExtrasEnumerable.AddRange(propertyExtras);
        await appDbContext.SaveChangesAsync();
    }
}
