using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Season.Put;

public record PutSeasonCommand : SeasonDto, ICommand<SeasonDto>
{
    public long SeasonId { get; init; }
};