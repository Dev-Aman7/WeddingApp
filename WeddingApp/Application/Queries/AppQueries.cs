using MediatR;
using WeddingApp.Application.DTOs;

namespace WeddingApp.Application.Queries;

public record GetGuestsQuery() : IRequest<List<GuestDto>>;
public record GetTodosQuery() : IRequest<List<TodoDto>>;