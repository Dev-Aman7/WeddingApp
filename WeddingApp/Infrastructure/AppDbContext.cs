using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeddingApp.Domain.Entities;

namespace WeddingApp.Infrastructure;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<Todo>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}

