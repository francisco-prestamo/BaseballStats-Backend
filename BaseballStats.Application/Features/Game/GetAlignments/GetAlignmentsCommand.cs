using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.GetAlignments;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetAlignmentsCommand : ICommand<AlignmentsDto>
{
    public long GameId { get; init; }
}