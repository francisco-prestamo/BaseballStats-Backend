using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.GetSubstitutions;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetSubstitutionsCommand : ICommand<SubstitutionsDto>
{
    public long GameId { get; init; }
}