using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Player.GetAllPlayers
{
    public record GetAllPlayersCommand : ICommand<List<RegularPlayerDto>>
    {}
}