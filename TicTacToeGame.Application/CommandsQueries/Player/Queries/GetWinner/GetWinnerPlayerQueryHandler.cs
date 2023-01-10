using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Application.Common.Game;
using TicTacToeGame.Application.Common.TicTacToeOptions;
using TicTacToeGame.Application.Interfaces;

namespace TicTacToeGame.Application.CommandsQueries.Player.Queries.GetWinner;

public class GetWinnerPlayerQueryHandler
    : IRequestHandler<GetWinnerPlayerQuery, string?>
{
    private readonly ITicTacToeDbContext _dbContext;
    private readonly TicTacToeWinners _ticTacToe;

    private const string Draw = "Draw";

    public GetWinnerPlayerQueryHandler(ITicTacToeDbContext dbContext,
        TicTacToeWinners ticTacToe)
    {
        _dbContext = dbContext;
        _ticTacToe = ticTacToe;
    }

    public async Task<string?> Handle(GetWinnerPlayerQuery request, CancellationToken cancellationToken)
    {
        var game = await _dbContext.TicTacToes
            .Include(g => g.Players)
            .FirstOrDefaultAsync(g => g.ConnectionId == request.ConnectionId, cancellationToken);
        if (game == null)
            throw new NullReferenceException($"This game does not exist!");
        var winnerStep = _ticTacToe.GetWinnerStep(game.Board);
        switch (winnerStep)
        {
            case StepTypes.CrossValue or StepTypes.ZeroValue:
            {
                var player = game.Players
                    .First(p => p.StepTypes == winnerStep);
                return player.Name;
            }
            case EndTypes.Draw:
                return EndTypes.Draw;
            default:
                return null;
        }
    }
}