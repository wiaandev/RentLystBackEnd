using System.ComponentModel.DataAnnotations.Schema;

namespace RentOutBackEnd.Domain.Entities;

public class RentDuration
{
    [ID]
    public int Id { get; set; }

    [ForeignKey(nameof(PropertyPost))] 
    public PropertyPost Property { get; set; } = null!;
    
    public int PropertyPostId { get; set; }
    
    public DateTime From { get; set; }

    public DateTime To { get; set; }
}