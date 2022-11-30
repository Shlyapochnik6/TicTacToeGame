using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Domain;

namespace TicTacToeGame.Application.Interfaces;

public interface ITicTacToeDbContext
{
    DbSet<Player> Players { get; set; }
    DbSet<TicTacToe> TicTacToes { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}