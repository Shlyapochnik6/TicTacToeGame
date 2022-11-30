using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Application.Interfaces;
using TicTacToeGame.Domain;

namespace TicTacToeGame.Persistence.Contexts;

public sealed class TicTacToeDbContext : DbContext, ITicTacToeDbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<TicTacToe> TicTacToes { get; set; }

    public TicTacToeDbContext(DbContextOptions<TicTacToeDbContext> options) 
        : base(options)
    {
        Database.EnsureCreated();
    }
}