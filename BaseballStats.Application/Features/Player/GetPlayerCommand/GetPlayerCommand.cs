using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Player.GetPlayer;

public record GetPlayerCommand : ICommand<RegularPlayerDto>
{
    public long Id { get; init; }
}