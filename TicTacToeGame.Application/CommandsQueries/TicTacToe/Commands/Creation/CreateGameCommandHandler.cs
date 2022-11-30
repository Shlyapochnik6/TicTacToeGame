using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Application.Common.TicTacToeOptions;
using TicTacToeGame.Application.Interfaces;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Creation;

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
{
    private readonly ITicTacToeDbContext _dbContext;
    
    public CreateGameCommandHandler(ITicTacToeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var author = await _dbContext.Players
            .FirstOrDefaultAsync(p => p.Name == request.AuthorName, cancellationToken);
        if (author == null)
        {
            throw new NullReferenceException($"{request.AuthorName} does not exist");
        }
        var game = new Domain.TicTacToe()
        {
            GameStatus = GamingSessions.Open,
            ConnectionId = Guid.NewGuid(),
            Board = BoardData.StartBoardData,
            PlayerMoveName = author.Name
        };
        await _dbContext.TicTacToes.AddAsync(game, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return game.ConnectionId;
    }
}