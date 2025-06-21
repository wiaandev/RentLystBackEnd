using RentroBackEnd.Domain.Entities;

namespace RentroBackEnd.Presentation.Types;

public class Me : User
{
    public Me(User? user)
    {
        this.User = user;
    }

    public User? User { get; set; }
}
