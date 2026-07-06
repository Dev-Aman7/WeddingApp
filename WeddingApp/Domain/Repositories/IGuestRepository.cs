using WeddingApp.Domain.Entities;

namespace WeddingApp.Domain.Repositories;

public interface IGuestRepository
{
    Task<List<Guest>> GetAllAsync();
    Task<Guest> AddAsync(Guest guest);
}