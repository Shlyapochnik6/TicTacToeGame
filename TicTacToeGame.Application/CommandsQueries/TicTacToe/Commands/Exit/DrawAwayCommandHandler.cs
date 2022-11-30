using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Application.Common.TicTacToeOptions;
using TicTacToeGame.Application.Interfaces;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Exit;

public class DrawAwayCommandHandler : IRequestHandler<DrawAwayCommand, Domain.TicTacToe>
{
    private readonly ITicTacToeDbContext _dbContext;

    public DrawAwayCommandHandler(ITicTacToeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.TicTacToe> Handle(DrawAwayCommand request, CancellationToken cancellationToken)
    {
        var game = await _dbContext.TicTacToes
            .Include(g => g.Players)
            .FirstOrDefaultAsync(g => g.ConnectionId == request.ConnectionId, cancellationToken);
        if (game == null)
        {
            throw new NullReferenceException($"The entered game does not exist");
        }
        var player = game.Players
            .First(p => p.Name == request.Name);
        game.Players.Remove(player);
        game.GameStatus = GamingSessions.Finished;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return game;
    }
}