using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Application.Interfaces;

namespace TicTacToeGame.Application.CommandsQueries.Player.Commands.Move;

public class MovePlayerCommandHandler : IRequestHandler<MovePlayerCommand, Domain.TicTacToe>
{
    private readonly ITicTacToeDbContext _dbContext;

    public MovePlayerCommandHandler(ITicTacToeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.TicTacToe> Handle(MovePlayerCommand request, CancellationToken cancellationToken)
    {
        var game = await _dbContext.TicTacToes
            .Include(g => g.Players)
            .FirstAsync(g => g.ConnectionId == request.ConnectionId, cancellationToken);
        var player = await _dbContext.Players
            .FirstAsync(p => p.Name == request.Name, cancellationToken);
        var step = player.StepTypes;
        game.Board[request.Position] = step;
        game.PlayerMoveName = game.PlayerMoveName == game.Players[0].Name ? game.Players[1].Name : game.Players[0].Name;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return game;
    }
}