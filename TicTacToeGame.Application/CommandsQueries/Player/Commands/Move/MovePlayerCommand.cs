using MediatR;

namespace TicTacToeGame.Application.CommandsQueries.Player.Commands.Move;

public class MovePlayerCommand : IRequest<Domain.TicTacToe>
{
    public string Name { get; set; }
    
    public Guid ConnectionId { get; set; }
    
    public int Position { get; set; }
}