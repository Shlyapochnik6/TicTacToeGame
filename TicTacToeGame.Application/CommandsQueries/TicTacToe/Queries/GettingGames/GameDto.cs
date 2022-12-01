using AutoMapper;
using TicTacToeGame.Application.Common.Mappings;

namespace TicTacToeGame.Application.CommandsQueries.TicTacToe.Queries.GettingGames;

public class GameDto : IMapWith<Domain.TicTacToe>
{
    public string Name { get; set; }
    
    public Guid ConnectionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.TicTacToe, GameDto>()
            .ForMember(g => g.ConnectionId,
                c => c.MapFrom(g => g.ConnectionId))
            .ForMember(g => g.Name,
                c => c.MapFrom(g => g.PlayerMoveName));
    }
}