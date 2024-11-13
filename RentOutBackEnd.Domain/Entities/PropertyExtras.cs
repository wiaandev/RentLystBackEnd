using System.ComponentModel.DataAnnotations.Schema;

namespace RentOutBackEnd.Domain.Entities;

public class PropertyExtras
{
    [ID]
    public int Id { get; set; }

    [ForeignKey(nameof(PropertyPost))] 
    public PropertyPost Property { get; set; } = null!;
    
    public int PropertyPostId { get; set; }
    
    public bool HasFiber { get; set; }
    
    public bool PetsAllowed { get; set; }
    
    public bool HasPool { get; set; }
    
    public bool HasGarden { get; set; }
    
    public bool HasPatio { get; set; }
    
    public bool HasFlatlet { get; set; }
}