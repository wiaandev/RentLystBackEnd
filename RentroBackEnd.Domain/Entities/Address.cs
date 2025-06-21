using System.ComponentModel.DataAnnotations.Schema;

namespace RentroBackEnd.Domain.Entities;

public class Address: INode
{

    [ID]
    public int Id { get; set; }

    public PropertyPost Property { get; set; } = null!;
    
    public int PropertyPostId { get; set; }

    public string StreetName { get; set; } = null!;

    public string StreetNumber { get; set; } = null!;

    public string Suburb { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Province { get; set; } = null!;
}