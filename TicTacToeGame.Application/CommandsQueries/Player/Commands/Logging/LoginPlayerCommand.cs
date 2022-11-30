using System.Security.Claims;
using MediatR;

namespace TicTacToeGame.Application.CommandsQueries.Player.Commands.Logging;

public class LoginPlayerCommand : IRequest<ClaimsIdentity>
{
    public string Name { get; set; }
}