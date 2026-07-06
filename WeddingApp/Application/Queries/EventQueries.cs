using MediatR;
using WeddingApp.Application.DTOs;

namespace WeddingApp.Application.Queries;

public record GetEventsQuery() : IRequest<List<EventResponseDto>>;

public record GetEventByIdQuery(int Id) : IRequest<EventResponseDto>;