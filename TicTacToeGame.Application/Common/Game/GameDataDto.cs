namespace TicTacToeGame.Application.Common.Game;

public class GameDataDto
{
    public string PlayerMoveName { get; set; }
    
    public string StepTypes { get; set; }
    
    public string[] Board { get; set; }
    
    public string GameStatus { get; set; }
}