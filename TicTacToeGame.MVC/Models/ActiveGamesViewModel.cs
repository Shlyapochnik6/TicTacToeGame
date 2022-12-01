using TicTacToeGame.Application.CommandsQueries.TicTacToe.Queries.GettingGames;

namespace TicTacToeGame.MVC.Models;

public class ActiveGamesViewModel
{
    public string? AuthorName { get; set; }
    public Guid? ConnectionId { get; set; }
    public List<GameDto>? Games { get; set; }
}