namespace RentOutBackEnd.Domain.Entities
{
    public class PropertyPost
    {
        [ID]
        public int Id { get; set; }

        public RentType PropertyType { get; set; }
        
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
        
        public enum AllowedPetType
        {
            Dog,
            Cat,
            Hamster,
            Bird,
            Fish,
        }
    }
}