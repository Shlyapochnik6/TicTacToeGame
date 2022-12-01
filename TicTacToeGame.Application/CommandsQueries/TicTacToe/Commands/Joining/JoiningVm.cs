using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Joining;

public class JoiningVm
{
    public Domain.TicTacToe TicTacToe { get; set; }
    public ModelStateDictionary ModelState { get; set; }
}