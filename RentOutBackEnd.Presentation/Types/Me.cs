using RentOutBackEnd.Domain.Entities;

namespace RentOutBackEnd.Presentation.Types;

public class Me: User
{
    public Me(User? user)
    {
        this.User = user;
    }
    
    public User? User { get; set; }
}