using FastEndpoints;
using BaseballStats.Application.DTOs;

namespace BaseballStats.Application.Features.Player.GetPlayer;

public record GetPlayerCommand : ICommand<RegularPlayerDto>
{
    public long Id { get; init; }
}