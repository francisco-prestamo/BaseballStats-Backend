using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Season.Delete;

public record DeleteSeasonCommand : ICommand<SeasonDto>
{
    public long Id { get; init; }
}