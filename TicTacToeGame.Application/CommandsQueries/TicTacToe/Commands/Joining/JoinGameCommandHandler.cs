using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Application.Common.TicTacToeOptions;
using TicTacToeGame.Application.Interfaces;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Joining;

public class JoinGameCommandHandler : IRequestHandler<JoinGameCommand, JoiningVm>
{
    private readonly ITicTacToeDbContext _dbContext;

    public JoinGameCommandHandler(ITicTacToeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<JoiningVm> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        var player = await _dbContext.Players
            .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);
        if (player == null)
            throw new NullReferenceException($"{request.Name} does not exist");
        Guid.TryParse(request.ConnectionId, out var connectionId);
        var game = await _dbContext.TicTacToes
            .Include(p => p.Players)
            .FirstOrDefaultAsync(g => g.ConnectionId == connectionId, cancellationToken);
        if (game == null)
        {
            request.ModelState.AddModelError("game-error", "The game was not created");
            return new JoiningVm() { ModelState = request.ModelState };
        }
        if (game.Players.Count >= 2)
        {
            request.ModelState.AddModelError("game-error", "The game session already taken");
            game.ConnectionId = Guid.Empty;
            return new JoiningVm() { TicTacToe = game, ModelState = request.ModelState };
        }
        if (game == null)
            throw new NullReferenceException($"The {nameof(game)} is null");
        switch (game.Players.Count)
        {
            case <= 0:
                player.StepTypes = player.Name == game.PlayerMoveName ? StepTypes.CrossValue : StepTypes.ZeroValue;
                game.Players.Add(player);
                break;
            case < 2:
                game.Players.All(p => p.Name != request.Name);
                player.StepTypes = player.Name == game.PlayerMoveName ? StepTypes.CrossValue : StepTypes.ZeroValue;
                game.Players.Add(player);
                break;
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new JoiningVm() { TicTacToe = game, ModelState = request.ModelState };
    }
}