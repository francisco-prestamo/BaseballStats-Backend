using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Player.Put;

public record UpdatePlayerCommand : ICommand<RegularPlayerDto>
{
    public long Id { get; init; }
    public string Name { get; init; } = null!;
    public int Age { get; init; }
    public int YearsOfExperience { get; init; }
    public double? BattingAverage { get; init; }
}