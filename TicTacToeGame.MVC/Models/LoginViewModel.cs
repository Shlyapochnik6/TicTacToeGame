using System.ComponentModel.DataAnnotations;

namespace TicTacToeGame.MVC.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "You haven't entered your name")]
    public string Name { get; set; }
}