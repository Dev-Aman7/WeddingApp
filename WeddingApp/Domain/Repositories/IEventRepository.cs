using WeddingApp.Domain.Entities;

namespace WeddingApp.Domain.Repositories;

public interface IEventRepository
{
    Task<List<Event>> GetAllAsync();
    Task<Event> AddAsync(Event eventEntity);
    Task<bool> DeleteAsync(int id);
    Task<Event?> GetByIdAsync(int id);
}