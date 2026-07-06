using WeddingApp.Domain.Entities;

namespace WeddingApp.Domain.Repositories;

public interface ITodoRepository
{
    Task<List<Todo>> GetAllAsync();
    Task<Todo> AddAsync(Todo todo);
    Task<bool> DeleteAsync(int id);
    Task<Todo?> GetByIdAsync(int id);
}