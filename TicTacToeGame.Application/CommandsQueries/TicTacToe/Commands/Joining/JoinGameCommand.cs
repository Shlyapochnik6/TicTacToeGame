using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Joining;

public class JoinGameCommand : IRequest<JoiningVm>
{
    public Guid? ConnectionId { get; set; }
    
    public string Name { get; set; }

    public bool IsValid { get; set; }
    
    public ModelStateDictionary ModelState { get; set; } = new();
}