using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Player.GetAll
{
    public record GetAllPlayersCommand : ICommand<List<RegularPlayerDto>>
    {}
}