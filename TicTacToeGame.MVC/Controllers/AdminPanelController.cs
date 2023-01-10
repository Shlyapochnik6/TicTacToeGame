using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Creation;
using TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Joining;
using TicTacToeGame.Application.CommandsQueries.TicTacToe.Queries.GettingGames;
using TicTacToeGame.MVC.Models;

namespace TicTacToeGame.MVC.Controllers;

[Authorize]
public class AdminPanelController : Controller
{
    private readonly IMediator _mediator;

    public AdminPanelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var query = new GetGamesQuery();
        var result = await _mediator.Send(query);
        var gamesDtos = new ActiveGamesViewModel()
        {
            Games = result.Games
        };
        return View(gamesDtos);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGame()
    {
        var command = new CreateGameCommand()
        {
            AuthorName = User.Identity!.Name!
        };
        var connectionId = await _mediator.Send(command);
        return RedirectToAction("Index", "TicTacToe",
            new { connectionId });
    }
    
    [HttpPost]
    public async Task<IActionResult> JoinGame(ActiveGamesViewModel model)
    {
        var command = new JoinGameCommand()
        {
            ConnectionId = model.ConnectionId,
            Name = User.Identity!.Name!,
            IsValid = true,
            ModelState = ModelState
        };
        var joining = await _mediator.Send(command);
        if (!joining.ModelState.IsValid)
            return View("Index");
        return RedirectToAction("Index", "TicTacToe",
            new { connectionId = joining.TicTacToe.ConnectionId });
    }
}