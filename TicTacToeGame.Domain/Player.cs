namespace TicTacToeGame.Domain;

public class Player
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string GameChip { get; set; } = "";

    public List<TicTacToe> TicTacToeGames { get; set; }
}