using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using TicTacToeGame.Application.Common.Game;
using TicTacToeGame.Application.Common.UserIdProviders;

namespace TicTacToeGame.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<TicTacToeWinners>();
        services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
        return services;
    }
}