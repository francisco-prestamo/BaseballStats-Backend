using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Pitcher.Delete;

public record DeletePitcherCommand : ICommand<PitcherDto>
{
    public long PlayerId { get; init; }
}