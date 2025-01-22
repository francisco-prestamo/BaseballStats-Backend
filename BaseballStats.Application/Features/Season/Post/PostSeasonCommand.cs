using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Season.Post;

public record PostSeasonCommand : ICommand<SeasonDto>;