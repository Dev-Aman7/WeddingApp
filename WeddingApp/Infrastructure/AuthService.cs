using Microsoft.AspNetCore.Identity;
using WeddingApp.Application.Handlers;

namespace WeddingApp.Infrastructure;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }

    public async Task<ApplicationUser> CreateUserAsync(string email, string password)
    {
        var identityUser = new IdentityUser
        {
            Email = email,
            UserName = email
        };

        var result = await _userManager.CreateAsync(identityUser, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return new ApplicationUser
        {
            Id = identityUser.Id,
            Email = identityUser.Email ?? string.Empty
        };
    }

    public async Task<ApplicationUser?> ValidateUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return null;

        var isValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isValid)
            return null;

        return new ApplicationUser
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty
        };
    }
}