using MediatR;
using WeddingApp.Application.Commands;
using WeddingApp.Application.DTOs;
using WeddingApp.Application.Queries;
using WeddingApp.Domain.Entities;
using WeddingApp.Domain.Repositories;

namespace WeddingApp.Application.Handlers;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoDto>
{
    private readonly ITodoRepository _todoRepository;

    public CreateTodoCommandHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken ct)
    {
        var todo = new Todo
        {
            Title = request.Request.Title,
            Status = TodoStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _todoRepository.AddAsync(todo);

        return new TodoDto
        {
            Id = created.Id,
            Title = created.Title,
            Status = created.Status,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };
    }
}

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
{
    private readonly ITodoRepository _todoRepository;

    public DeleteTodoCommandHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken ct)
    {
        return await _todoRepository.DeleteAsync(request.Id);
    }
}


public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, List<TodoDto>>
{
    private readonly ITodoRepository _todoRepository;

    public GetTodosQueryHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<List<TodoDto>> Handle(GetTodosQuery request, CancellationToken ct)
    {
        var todos = await _todoRepository.GetAllAsync();
        return todos.Select(t => new TodoDto
        {
            Id = t.Id,
            Title = t.Title,
            Status = t.Status,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        }).ToList();
    }
}