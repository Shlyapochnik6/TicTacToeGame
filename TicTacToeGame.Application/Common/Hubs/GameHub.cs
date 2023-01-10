using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TicTacToeGame.Application.CommandsQueries.Player.Commands.PlayerStep;
using TicTacToeGame.Application.CommandsQueries.Player.Queries.GetWinner;
using TicTacToeGame.Application.CommandsQueries.TicTacToe.Commands.Joining;
using TicTacToeGame.Application.CommandsQueries.TicTacToe.Queries.GettingGames;
using TicTacToeGame.Application.Common.Game;
using TicTacToeGame.Application.Common.TicTacToeOptions;
using TicTacToeGame.Application.Interfaces;

namespace TicTacToeGame.Application.Common.Hubs;

[Authorize]
public class GameHub : Hub
{
    private readonly IMediator _mediator;
    private readonly ITicTacToeDbContext _dbContext;

    public GameHub(IMediator mediator, ITicTacToeDbContext context)
    {
        _mediator = mediator;
        _dbContext = context;
    }

    public async Task Connect(Guid connectionId)
    {
        var playerName = Context.User?.Identity?.Name!;
        var joinGameCommand = new JoinGameCommand()
        {
            ConnectionId = connectionId,
            Name = playerName
        };
        var joiningVm = await _mediator.Send(joinGameCommand);
        var query = new GetWinnerPlayerQuery() { ConnectionId = joiningVm.TicTacToe.ConnectionId };
        var winnerPlayer = await _mediator.Send(query);
        if (joiningVm.TicTacToe.Players.Count >= 2)
        {
            if (joiningVm.TicTacToe.Players.Count <= 1
                && joiningVm.TicTacToe.GameStatus != GamingSessions.Finished)
                await SendAllGame();
            await Send(joiningVm.TicTacToe);
            if (winnerPlayer != null)
                await SendWinner(joiningVm.TicTacToe.Players, winnerPlayer);
        }
    }
    
    public async Task PlayerStep(Guid connectionId, int positionField)
    {
        var playerName = Context.User?.Identity?.Name!;
        var playerStepCommand = new PlayerStepCommand(playerName, connectionId, positionField);
        var game = await _mediator.Send(playerStepCommand);
        var query = new GetWinnerPlayerQuery() { ConnectionId = game.ConnectionId };
        var winnerPlayer = await _mediator.Send(query);
        await Send(game);
        if (winnerPlayer != null)
            await SendWinner(game.Players, winnerPlayer);
    }

    public async Task SendAllGame()
    {
        var getAllGameQuery = new GetGamesQuery();
        var gameDtos = await _mediator.Send(getAllGameQuery);
        await Clients.All.SendAsync("GetAllGame", gameDtos);
    }

    public async Task GetPlayerName()
    {
        var playerName = Context.User?.Identity?.Name!;
        await Clients.User(playerName).SendAsync("GetPlayerName", playerName);
    }

    private async Task Send(Domain.TicTacToe game)
    {
        foreach (var player in game.Players)
        {
            await Clients.Users(player.Name)
                .SendAsync("GetConnectionInfo", new GameDataDto()
                {
                    PlayerMoveName = game.PlayerMoveName,
                    StepTypes = player.StepTypes,
                    Board = game.Board,
                    GameStatus = game.GameStatus
                });
        }
    }

    private async Task SendWinner(IEnumerable<Domain.Player> players, string winnerPlayer)
    {
        foreach (var player in players)
        {
            await Clients.Users(player.Name)
                .SendAsync("GetWinnerPlayer", winnerPlayer, player.Name == winnerPlayer);
        }
    }
}