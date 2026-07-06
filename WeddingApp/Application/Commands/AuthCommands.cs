using MediatR;
using WeddingApp.Application.DTOs;

namespace WeddingApp.Application.Commands;

public record SignUpCommand(SignUpRequest Request) : IRequest<AuthResponse>;

public record LoginCommand(LoginRequest Request) : IRequest<AuthResponse>;