using Microsoft.AspNetCore.Identity;
using RentOutBackEnd.Domain;
using RentOutBackEnd.Domain.Entities;

namespace RentOutBackEnd.Presentation.Mutations;

[MutationType]
public class UserMutations
{
    public async Task<User?> Login([Service] UserManager<User> userManager, [Service] SignInManager<User> signInManager, string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null || user.IsDeleted)
        {
            throw new Exception("Unauthorized");
        }

        signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;

        var result = await signInManager.PasswordSignInAsync(email, password, true, lockoutOnFailure: true);
        
        Console.WriteLine(result);

        if (!result.Succeeded)
        {
            throw new Exception("Unauthorized");
        }

        return user;
    }
    
    public async Task<bool> Logout([Service] UserManager<User> userManager, [Service] SignInManager<User> signInManager, string email, string password)
    {
        await signInManager.SignOutAsync();

        return true;
    }
    
    public async Task<User> UserRegistration(AppDbContext dbContext, [Service] UserManager<User> userManager, UserRegistrationRequest input,
        [Service] IServiceProvider serviceProvider)
    {

        var userStore = serviceProvider.GetRequiredService<IUserStore<User>>();
        var emailStore = (IUserEmailStore<User>)userStore;
        var email = input.Email;

        if (string.IsNullOrEmpty(email))
        {
            throw new Exception("Email address not valid");
        }

        var user = new User
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email
        };
        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        var result = await userManager.CreateAsync(user, input.Password);

        if (!result.Succeeded)
        {
            throw new Exception("Request has failed");
        }
        
        
        if (result.Succeeded)
        {
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Email = input.Email;
            user.UserName = input.Email;
            user.EmailConfirmed = true;
            if (input.PhoneNumber != null)
            {
                user.PhoneNumberConfirmed = true;
            }

            await userManager.AddPasswordAsync(user, input.Password);
            await userManager.SetPhoneNumberAsync(user, input.PhoneNumber);

            var newResult = await userManager.UpdateAsync(user);

            if (newResult.Succeeded)
            {
                    await dbContext.SaveChangesAsync();
                return user;
            }

            throw new Exception("User update failed, please try again later.");
        }

        return user;
    }

    public record UserRegistrationRequest
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        
        public string Password { get; init; }
        
        public string ConfirmPassword { get; init; }
        
        public string PhoneNumber { get; init; }
    }
}