using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Game.GetGamesFromSeries;

public class GetGamesFromSeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetGamesFromSeriesCommand, List<GameDto>>
{
    public override async Task<List<GameDto>> ExecuteAsync(GetGamesFromSeriesCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var seriesId = command.SeriesId;

        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();

        // we will do this in order to get the teams for the games only once
        // instead of obtaining a copy of each team's data for each game
        // this also allows us to not change the existing Domain

        // get team ids for teams that have played in the series
        var relevantTeamIds = gameRepository.Where(x => x.SeriesId == seriesId)
            .Select(x => x.Team1Id)
            .Union(gameRepository.Where(x => x.SeriesId == seriesId)
                .Select(x => x.Team2Id))
            .Distinct()
            .ToList();

        // get teams for the series
        var relevantTeams = teamRepository.Where(x => relevantTeamIds.Contains(x.Id)).Select(x => x.ToDto())
           .ToList().ToDictionary(x => x.id);

        // games for the series
        var games = gameRepository.Where(x => x.SeriesId == seriesId);

        var gamesDto = games.Select(
            x => x.ToDto(
                relevantTeams[x.Team1Id], 
                relevantTeams[x.Team2Id]
            )
        );

        return gamesDto.ToList();
    }

    private async Task DatabaseValidations(GetGamesFromSeriesCommand command)
    {
        var seasonRepository = unitOfWork.Repository<Domain.Entities.Season>();
        var season = await seasonRepository.GetByIdAsync(command.SeasonId);

        if (season is null)
            ThrowError("SeasonId not found", StatusCodes.Status404NotFound);

        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();
        var series = await seriesRepository.GetByIdAsync(command.SeriesId);

        if (series is null)
            ThrowError("SeriesId not found", StatusCodes.Status404NotFound);
    }
}