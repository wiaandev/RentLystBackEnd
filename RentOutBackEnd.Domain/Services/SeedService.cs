using Microsoft.AspNetCore.Identity;
using RentOutBackEnd.Domain.Entities;
using RentOutBackEnd.Presentation;

namespace RentOutBackEnd.Domain.Services;

public class SeedService(AppDbContext appDbContext, UserManager<User> userManager)
{
    public async Task Seed()
    {
        await this.SeedPropertyPosts();
    }

    public async Task SeedPropertyPosts()
    {
        var properties = new List<PropertyPost>()
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
                CreatedAt = DateTime.UtcNow.AddHours(2) // Set CreatedAt here
            }
        };

        // Add each property to the context to ensure it is saved to the database
        foreach (var property in properties)
        {
            appDbContext.PropertyPosts.Add(property);
        }

        await appDbContext.SaveChangesAsync(); // Save changes to persist the data
    }
}