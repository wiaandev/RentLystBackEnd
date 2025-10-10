using HotChocolate.Authorization;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Entities;

namespace RentlystBackEnd.Presentation.Mutations;

[MutationType]
public class PropertyMutations
{
    [Authorize(Roles = ["Seller"])]
    public async Task<bool> AddProperty(
        PropertyPost propertyPost,
        AppDbContext appDbContext)
    {
        var property = new PropertyPost
        {
            PropertyType = propertyPost.PropertyType,
            WeeklyAmount = propertyPost.WeeklyAmount,
            BedroomAmount = propertyPost.BedroomAmount,
            BathroomAmount = propertyPost.BathroomAmount,
            ParkingAmount = propertyPost.ParkingAmount,
            PetAmount = propertyPost.PetAmount,
            PetType = propertyPost.PetType,
            CreatedAt = default,
            Address = new Address
            {
                StreetName = propertyPost.Address.StreetName,
                StreetNumber = propertyPost.Address.StreetNumber,
                Suburb = propertyPost.Address.Suburb,
                City = propertyPost.Address.City,
                Province = propertyPost.Address.Province,
            }
        };
        appDbContext.Add(property);
        await appDbContext.SaveChangesAsync();
        return true;
    }
}