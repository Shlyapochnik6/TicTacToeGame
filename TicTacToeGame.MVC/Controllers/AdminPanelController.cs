using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Creation;
using TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Joining;
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
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGame()
    {
        var command = new CreateGameCommand()
        {
            AuthorName = User.Identity!.Name!
        };
        var connectionId = await _mediator.Send(command);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> JoinGame(JoinViewModel model)
    {
        var command = new JoinGameCommand()
        {
            ConnectionId = model.ConnectionId,
            Name = User.Identity!.Name!,
            ModelState = ModelState
        };
        var joining = await _mediator.Send(command);
        if (!joining.ModelState.IsValid)
        {
            return View("Index", model);
        }
        return RedirectToAction("Index");
    }
}