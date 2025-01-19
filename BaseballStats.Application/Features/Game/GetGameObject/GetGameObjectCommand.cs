using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.GetGameObject;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetGameObjectCommand : ICommand<GameWithTeamsDto>
{
    public long GameId { get; init; }
}