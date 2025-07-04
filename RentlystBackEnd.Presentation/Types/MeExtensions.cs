using System.Security.Claims;
using Microsoft.VisualBasic;
using RentlystBackEnd.Presentation.Types;

namespace RentOutBackEnd.Presentation.Types;

[ExtendObjectType<Me>]
public class MeExtensions
{
    public static bool IsSuperAdmin(
        ClaimsPrincipal claims)
    {
        // return claims.IsInRole();
        return true;
    }
}