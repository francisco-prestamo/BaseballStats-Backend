using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Season.GetSeasons
{
    public record GetSeasonsCommand : ICommand<List<SeasonDto>>
    {}
}