namespace TicTacToeGame.Domain;

public class TicTacToe
{
    public long Id { get; set; }
    
    public string[] Field { get; set; }
    
    public Guid ConnectionId { get; set; }
    
    public string GameStatus { get; set; }
    
    public string PlayerMove { get; set; }
    
    public List<Player> Players { get; set; }
}