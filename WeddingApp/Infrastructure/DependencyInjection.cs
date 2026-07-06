using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingApp.Application.Handlers;
using WeddingApp.Domain.Repositories;
using WeddingApp.Infrastructure.Repositories;

namespace WeddingApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            "server=localhost;port=3306;database=wedding;user=wedding_user;password=wedding_pass";

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IEventRepository, EventRepository>();

        return services;
    }
}
