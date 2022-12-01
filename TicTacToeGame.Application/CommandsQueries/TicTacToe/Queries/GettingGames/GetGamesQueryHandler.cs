using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Application.Common.TicTacToeOptions;
using TicTacToeGame.Application.Interfaces;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Queries.GettingGames;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, GamesVm>
{
    private readonly ITicTacToeDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGamesQueryHandler(ITicTacToeDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<GamesVm> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var games = await _dbContext.TicTacToes
            .Include(p => p.Players)
            .Where(p => p.GameStatus == GamingSessions.Waiting)
            .ProjectTo<GameDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        return new GamesVm { Games = games };
    }
}