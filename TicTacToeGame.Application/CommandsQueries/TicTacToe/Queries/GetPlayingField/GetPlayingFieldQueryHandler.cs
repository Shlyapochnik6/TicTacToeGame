using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Application.Interfaces;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Queries.GetPlayingField;

public class GetPlayingFieldQueryHandler : IRequestHandler<GetPlayingFieldQuery, string[]>
{
    private readonly ITicTacToeDbContext _context;

    public GetPlayingFieldQueryHandler(ITicTacToeDbContext context)
    {
        _context = context;
    }
    
    public async Task<string[]> Handle(GetPlayingFieldQuery request, CancellationToken cancellationToken)
    {
        var ticTacToe = await _context.TicTacToes
            .FirstOrDefaultAsync(t => t.ConnectionId == Guid.Parse(request.ConnectionId),
                cancellationToken);
        if (ticTacToe is null)
            throw new NullReferenceException("The lobby does not exist");
        return ticTacToe.Board!;
    }
}