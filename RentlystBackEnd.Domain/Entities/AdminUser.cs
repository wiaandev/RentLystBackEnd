using System.ComponentModel.DataAnnotations.Schema;

namespace RentlystBackEnd.Domain.Entities;

public class AdminUser
{
    [ID]
    public int Id { get; set; }
    
    public User User { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
}