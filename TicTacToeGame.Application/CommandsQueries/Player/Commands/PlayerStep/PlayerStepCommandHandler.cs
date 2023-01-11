using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Application.Interfaces;

namespace TicTacToeGame.Application.CommandsQueries.Player.Commands.PlayerStep;

public class PlayerStepCommandHandler : IRequestHandler<PlayerStepCommand, Domain.TicTacToe>
{
    private readonly ITicTacToeDbContext _dbContext;

    public PlayerStepCommandHandler(ITicTacToeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.TicTacToe> Handle(PlayerStepCommand request, CancellationToken cancellationToken)
    {
        var player = await _dbContext.Players
            .FirstAsync(p => p.Name == request.PlayerName, cancellationToken);
        var game = await _dbContext.TicTacToes.Include(g => g.Players)
            .FirstAsync(g => g.ConnectionId == request.ConnectionId, cancellationToken);
        var playerStep = player.StepTypes;
        game.Board[request.PositionField] = playerStep;
        game.PlayerMoveName = game.PlayerMoveName == game.Players[0].Name ? game.Players[1].Name
            : game.Players[0].Name;
        await _dbContext.SaveChangesAsync(new CancellationToken());
        return game;
    }
}