using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Application.Interfaces;

namespace TicTacToeGame.Application.CommandsQueries.Player.Commands.Logging;

public class LoginPlayerCommandHandler : IRequestHandler<LoginPlayerCommand, ClaimsIdentity>
{
    private readonly ITicTacToeDbContext _dbContext;

    public LoginPlayerCommandHandler(ITicTacToeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ClaimsIdentity> Handle(LoginPlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _dbContext.Players
            .FirstOrDefaultAsync(u => u.Name == request.Name, cancellationToken: cancellationToken); 
        if (player == null)
        {
            player = new Domain.Player()
            {
                Name = request.Name
            };
            await _dbContext.Players.AddAsync(player, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, player.Name)
        };
        var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }
}