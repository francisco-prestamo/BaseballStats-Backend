using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Player.GetPlayerOrPitcher
{
    public record GetPlayerOrPitcherCommand : ICommand<PlayerOrPitcherDto>
    {
        public long Id { get; init; }
    }
}