using MediatR;
using WeddingApp.Application.DTOs;

namespace WeddingApp.Application.Commands;


public record CreateEventCommand(CreateEventRequestDto Request) : IRequest<EventResponseDto>;