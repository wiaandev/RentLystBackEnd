using System.ComponentModel.DataAnnotations.Schema;

namespace RentlystBackEnd.Domain.Entities
{
    public class PropertyExtras
    {
        [ID]
        public int Id { get; set; }

        // Foreign Key Property
        [ForeignKey(nameof(Property))]
        public int PropertyPostId { get; set; }

        // Navigation Property (optional if you may create PropertyExtras independently)
        public PropertyPost Property { get; set; } = null!;

        public bool HasFiber { get; set; }
        
        public bool PetsAllowed { get; set; }
        
        public bool HasPool { get; set; }
        
        public bool HasGarden { get; set; }
        
        public bool HasPatio { get; set; }
        
        public bool HasFlatlet { get; set; }
    }
}