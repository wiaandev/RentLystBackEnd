using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RentOutBackEnd.Domain.Entities;

namespace RentOutBackEnd.Domain.Services;

public class UserService(UserManager<User> userManager)
{
    public async Task<User> GetByClaimsPrincipalAsync(ClaimsPrincipal principal)
    {
        if (principal == null || principal.Identity?.IsAuthenticated == false)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userId = userManager.GetUserId(principal);
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException("User ID not found in claims.");
        }

        var user = await userManager.FindByIdAsync(userId);
        if (user == null || user.IsDeleted)
        {
            throw new UnauthorizedAccessException("User not found or deleted.");
        }

        return user;
    }
}