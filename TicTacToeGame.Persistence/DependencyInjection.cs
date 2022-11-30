using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToeGame.Application.Interfaces;
using TicTacToeGame.Persistence.Contexts;

namespace TicTacToeGame.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<TicTacToeDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<ITicTacToeDbContext, TicTacToeDbContext>();
        return services;
    }
}