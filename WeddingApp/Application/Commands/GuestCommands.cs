using MediatR;
using WeddingApp.Application.DTOs;

namespace WeddingApp.Application.Commands;

public record CreateGuestCommand(CreateGuestRequest Request) : IRequest<GuestDto>;