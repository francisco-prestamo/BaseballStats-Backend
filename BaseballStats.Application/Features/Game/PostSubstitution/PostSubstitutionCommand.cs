using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.PostSubstitution;

// ReSharper disable once ClassNeverInstantiated.Global
public record PostSubstitutionCommand : ICommand<SingleSubstitutionCRUDDto>
{
    public long GameId { get; init; }
    public long TeamId { get; init; }
    public long PlayerInId { get; init; }
    public long PlayerOutId { get; init; }
    public TimeSpan Time { get; init; }
}