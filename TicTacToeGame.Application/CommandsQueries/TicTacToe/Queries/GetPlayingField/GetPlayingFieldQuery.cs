using MediatR;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Queries.GetPlayingField;

public class GetPlayingFieldQuery : IRequest<string[]>
{
    public string ConnectionId { get; set; }

    public GetPlayingFieldQuery(string connectionId)
    {
        ConnectionId = connectionId;
    }
}