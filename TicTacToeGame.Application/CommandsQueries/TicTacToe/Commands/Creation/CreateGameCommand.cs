using MediatR;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Creation;

public class CreateGameCommand : IRequest<Guid>
{
    public string AuthorName { get; set; }
}