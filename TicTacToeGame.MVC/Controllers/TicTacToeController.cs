using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToeGame.MVC.Controllers;

[Authorize]
public class TicTacToeController : Controller
{
    [HttpGet]
    public IActionResult Index(Guid connectionId)
    {
        return View(connectionId);
    }
}