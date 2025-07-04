using System.ComponentModel.DataAnnotations.Schema;

namespace RentlystBackEnd.Domain.Entities;

public class PropertyImage
{
    [ID]
    public int Id { get; set; }

    [ForeignKey(nameof(PropertyPost))] 
    public PropertyPost Property { get; set; } = null!;
    
    public int PropertyPostId { get; set; }

    public string Uri { get; set; } = null!;
    
    public bool IsPrimary { get; set; }
    
    public DateTime CreatedAt { get; set; }
}