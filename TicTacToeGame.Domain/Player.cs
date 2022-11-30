﻿namespace TicTacToeGame.Domain;

public class Player
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string GameChip { get; set; } = "";

    public List<TicTacToe> TicTacToes { get; set; }
}