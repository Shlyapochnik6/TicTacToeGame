using MediatR;

namespace TicTacToeGame.Application.CommandsQueries.Player.Commands.PlayerStep;

public class PlayerStepCommand : IRequest<Domain.TicTacToe>
{
    public string PlayerName { get; set; }
    public Guid ConnectionId { get; set; }
    public int PositionField { get; set; }

    public PlayerStepCommand(string playerName, Guid connectionId,
        int positionField)
    {
        PlayerName = playerName;
        ConnectionId = connectionId;
        PositionField = positionField;
    }
}