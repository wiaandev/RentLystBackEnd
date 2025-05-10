using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RentOutBackEnd.Domain.Entities;


public class User: IdentityUser<int>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    
    public bool IsDeleted { get; set; } = false;
    
    public AdminUser? AdminUser { get; set; }
    
    public bool IsRenter { get; set; }

    [ForeignKey(nameof(PropertyPost))] 
    public PropertyPost Property { get; set; } = null!;
    
    public int PropertyPostId { get; set; }
}