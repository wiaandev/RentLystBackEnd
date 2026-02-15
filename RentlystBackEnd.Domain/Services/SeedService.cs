using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain.Entities;

namespace RentlystBackEnd.Domain.Services;

public class SeedService(AppDbContext appDbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
{
    public async Task Seed()
    {
        await this.SeedRolesAsync();
        await this.SeedUsers();
        await this.SeedPropertyPosts();
        await this.SeedPropertyExtras();
        await this.SeedPropertyAddresses();
    }

    public async Task SeedRolesAsync()
    {
        var roles = new[] { "Admin", "Renter", "Seller" };

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new Role { Name = roleName, NormalizedName = roleName.ToUpper() };
                await roleManager.CreateAsync(role);
            }
        }
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
                }, "E!1vmpwd"
            ),
            (
                new User
                {
                    UserName = "charleneduvenhage@gmail.com",
                    Email = "charleneduvenhage@gmail.com",
                    FirstName = "Charlene",
                    LastName = "Duvenhage",
                    IsRenter = false,
                }, "E!1vmpwd"
            ),
            (
                new User
                {
                    UserName = "seller@mail.com",
                    Email = "seller@mail.com",
                    FirstName = "Tom",
                    LastName = "Bradley",
                    IsRenter = false,
                }, "E!1vmpwd"
            ),
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
        
        await userManager.AddToRoleAsync(users[0].user, "Renter");
        await userManager.AddToRoleAsync(users[1].user, "Admin");
        await userManager.AddToRoleAsync(users[2].user, "Seller");
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
                SellerId = 1, // resolved FK
            },
            new()
            {
                PropertyType = PropertyPost.RentType.House,
                WeeklyAmount = 2000,
                BedroomAmount = 3,
                BathroomAmount = 2,
                ParkingAmount = 4,
                CreatedAt = DateTime.UtcNow,
                SellerId = 2,
            }
        };

        appDbContext.PropertyPosts.AddRange(properties);
        await appDbContext.SaveChangesAsync();
    }
    
    public async Task SeedPropertyAddresses()
    {
        var addresses = new List<Address>
        {
            new()
            {
                StreetName = "Main Street",
                StreetNumber = "23",
                Suburb = "Lakeview",
                City = "Welkom",
                Province = "Free State",
                PropertyPostId = 1,
            },
            new()
            {
                StreetName = "Oak Avenue",
                StreetNumber = "12B",
                Suburb = "Riverside",
                City = "Bloemfontein",
                Province = "Free State",
                PropertyPostId = 2,
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
