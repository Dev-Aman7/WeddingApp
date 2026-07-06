using Microsoft.EntityFrameworkCore;
using WeddingApp.Domain.Entities;
using WeddingApp.Domain.Repositories;

namespace WeddingApp.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await _context.Set<Event>().ToListAsync();
    }

    public async Task<Event> AddAsync(Event eventEntity)
    {
        _context.Set<Event>().Add(eventEntity);
        await _context.SaveChangesAsync();
        return eventEntity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var eventEntity = await _context.Set<Event>().FindAsync(id);
        if (eventEntity == null)
            return false;

        _context.Set<Event>().Remove(eventEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Set<Event>().FindAsync(id);
    }
}

