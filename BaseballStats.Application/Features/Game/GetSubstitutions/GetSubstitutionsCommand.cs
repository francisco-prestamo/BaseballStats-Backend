using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.GetSubstitutions;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetSubstitutionsCommand : ICommand<GameSubstitutionsDto>
{
    public long GameId { get; init; }
}