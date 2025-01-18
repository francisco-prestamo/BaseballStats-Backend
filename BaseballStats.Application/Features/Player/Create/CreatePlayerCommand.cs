using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Player.Create;

public record CreatePlayerCommand : ICommand<RegularPlayerDto>
{
    public string Name { get; init; } = null!;
    public int Age { get; init; }
    public int YearsOfExperience { get; init; }
    public double? BattingAverage { get; init; }
}