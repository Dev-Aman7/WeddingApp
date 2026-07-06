using WeddingApp.Domain.Entities;

namespace WeddingApp.Application.DTOs;

public class TodoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TodoStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateTodoRequest
{
    public required string Title { get; set; }
}