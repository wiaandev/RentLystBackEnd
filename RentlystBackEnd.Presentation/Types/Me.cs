using RentlystBackEnd.Domain.Entities;

namespace RentlystBackEnd.Presentation.Types;

public class Me : User
{
    public Me(User? user)
    {
        this.User = user;
    }

    public User? User { get; set; }
}
