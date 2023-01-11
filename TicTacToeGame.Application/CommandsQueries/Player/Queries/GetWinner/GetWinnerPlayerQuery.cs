using MediatR;

namespace TicTacToeGame.Application.CommandsQueries.Player.Queries.GetWinner;

public class GetWinnerPlayerQuery : IRequest<string?>
{
    public Guid ConnectionId { get; set; }
}