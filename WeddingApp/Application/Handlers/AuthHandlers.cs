using MediatR;
using WeddingApp.Application.Commands;
using WeddingApp.Application.DTOs;

namespace WeddingApp.Application.Handlers;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user);
}

public interface IAuthService
{
    Task<bool> UserExistsAsync(string email);
    Task<ApplicationUser> CreateUserAsync(string email, string password);
    Task<ApplicationUser?> ValidateUserAsync(string email, string password);
}

public class ApplicationUser
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, AuthResponse>
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public SignUpCommandHandler(IAuthService authService, IJwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(SignUpCommand request, CancellationToken ct)
    {
        if (await _authService.UserExistsAsync(request.Request.Email))
        {
            throw new InvalidOperationException("User already exists");
        }

        var user = await _authService.CreateUserAsync(request.Request.Email, request.Request.Password);
        var token = _jwtService.GenerateToken(user);

        return new AuthResponse { Token = token };
    }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(IAuthService authService, IJwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await _authService.ValidateUserAsync(request.Request.Email, request.Request.Password);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = _jwtService.GenerateToken(user);
        return new AuthResponse { Token = token };
    }
}