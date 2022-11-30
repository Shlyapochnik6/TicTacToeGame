using MediatR;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Exit;

public class DrawAwayCommand : IRequest<Domain.TicTacToe>
{
    public string Name { get; set; }
    
    public Guid ConnectionId { get; set; }
}