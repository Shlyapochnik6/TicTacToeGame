using TicTacToeGame.Application.Common.TicTacToeOptions;
using TicTacToeGame.Application.Extensions;

namespace TicTacToeGame.Application.Common.Game;

public class TicTacToeWinners
{
    private const int MapSize = 3;

    public string? GetWinnerStep(string[] playingBoard)
    {
        if (playingBoard.All(e => e != StepTypes.EmptyValue))
            return EndTypes.Draw;
        var map = playingBoard.ConvertToMatrix(MapSize);
        var searchResult = new List<string?>
        {
            SearchVerticalWinner(map),
            SearchHorizontalWinner(map),
            SearchDiagonalWinner(map)
        };
        var winner = searchResult
            .FirstOrDefault(r => r != null);
        return winner;
    }

    private string? SearchVerticalWinner(string[,] map)
    {
        for (var i = 0; i < MapSize; i++)
        {
            var column = map.GetColumn(i);
            var winner = GetStepPlayerWinner(column);
            if (winner != null)
                return winner;
        }
        return null;
    }

    private string? SearchHorizontalWinner(string[,] map)
    {
        for (var i = 0; i < MapSize; i++)
        {
            var row = map.GetRow(i);
            var winner = GetStepPlayerWinner(row);
            if (winner != null)
                return winner;
        }
        return null;
    }

    private string? SearchDiagonalWinner(string[,] map)
    {
        var principalDiagonal = map.GetDiagonal();
        var secondaryDiagonal = map.GetDiagonal(false);
        var winner = GetStepPlayerWinner(principalDiagonal);
        if (winner != null)
            return winner;
        winner = GetStepPlayerWinner(secondaryDiagonal);
        return winner;
    }

    private static string? GetStepPlayerWinner(string[] sequence)
    {
        if (sequence.All(e => e == StepTypes.CrossValue))
            return StepTypes.CrossValue;
        if (sequence.All(e => e == StepTypes.ZeroValue))
            return StepTypes.ZeroValue;
        return null;
    }
}