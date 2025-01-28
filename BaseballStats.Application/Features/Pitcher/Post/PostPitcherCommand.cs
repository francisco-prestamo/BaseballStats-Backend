using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Pitcher.Post;

public record PostPitcherCommand : ICommand<PitcherDto>
{
    public long PlayerId { get; init; }
    public int GamesWonNumber { get; init; }
    public int GamesLostNumber { get; init; }
    public bool RightHanded { get; init; }
    public double AllowedRunsAvg { get; init; }
    public double Effectiveness { get; init; }
}