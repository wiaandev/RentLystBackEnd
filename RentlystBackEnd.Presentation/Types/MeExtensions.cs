using System.Security.Claims;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentlystBackEnd.Domain;
using RentlystBackEnd.Domain.Entities;
using RentlystBackEnd.Presentation.Dataloaders;

namespace RentlystBackEnd.Presentation.Types;

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