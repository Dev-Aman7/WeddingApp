using MediatR;
using WeddingApp.Application.DTOs;
using WeddingApp.Domain.Entities;

namespace WeddingApp.Application.Commands;

public record CreateTodoCommand(CreateTodoRequest Request) : IRequest<TodoDto>;
public record DeleteTodoCommand(int Id) : IRequest<bool>;