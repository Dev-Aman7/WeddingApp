namespace WeddingApp.Domain.Entities;

public enum TodoStatus
{
    Pending = 0,
    InProgress = 1,
    Completed = 2
}

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TodoStatus Status { get; set; } = TodoStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}