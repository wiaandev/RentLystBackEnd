using System.ComponentModel.DataAnnotations.Schema;

namespace RentroBackEnd.Domain.Entities
{
    public class PropertyPost
    {
        [ID]
        public int Id { get; set; }

        public RentType PropertyType { get; set; }
        
        public int SellerId { get; set; }
        
        [ForeignKey(nameof(SellerId))]
        public User Seller { get; set; } = null!;
        
        public int WeeklyAmount { get; set; }
        
        public int BedroomAmount { get; set; }
        
        public int BathroomAmount { get; set; }
        
        public int ParkingAmount { get; set; }
        
        public int? PetAmount { get; set; }
        
        public List<AllowedPetType>? PetType { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public Address Address { get; set; } = null!;
        
        // Enum types within the class
        public enum RentType
        {
            Apartment,
            House,
            Flat,
            Plot,
            Duplex,
            Townhouse,
        }
        
        [Flags]
        public enum AllowedPetType
        {
            Dog = 0,
            Cat = 1,
            Bird = 2
        }
    }
}