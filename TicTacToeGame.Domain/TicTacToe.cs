namespace TicTacToeGame.Domain;

public class TicTacToe
{
    public long Id { get; set; }
    
    public string[] Board { get; set; }
    
    public Guid ConnectionId { get; set; }
    
    public string GameStatus { get; set; }
    
    public string PlayerMoveName { get; set; }
    
    public List<Player> Players { get; set; }
}