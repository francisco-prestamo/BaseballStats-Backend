using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Features.Team.GetTeamGamesInThisSeries;

public class GetTeamGamesInThisSeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetTeamGamesInThisSeriesCommand, List<GameWithTeamsDto>>
{
    public override async Task<List<GameWithTeamsDto>> ExecuteAsync(GetTeamGamesInThisSeriesCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();

        var seriesId = command.SeriesId;
        var teamId = command.TeamId;

        // get team ids for teams that have played in the series
        var relevantTeamIds = gameRepository.Where(x => x.SeriesId == seriesId)
            .Select(x => x.Team1Id)
            .Union(gameRepository.Where(x => x.SeriesId == seriesId)
            .Select(x => x.Team2Id))
            .Distinct()
            .ToList();

        // get teams for the series
        var relevantTeams = teamRepository.Where(x => relevantTeamIds.Contains(x.Id)).Select(x => x.ToDto())
           .ToList().ToDictionary(x => x.Id);

        // games for the series
        var games = (
            from g in unitOfWork.Repository<Domain.Entities.Game>().DbSet
            join s in unitOfWork.Repository<Domain.Entities.Series>().DbSet on g.SeriesId equals s.Id
            where g.SeriesId == seriesId && (g.Team1Id == teamId || g.Team2Id == teamId)
            select new {game = g, s.SeasonId}
        ).AsNoTracking();

        var gamesDto = games.Select(
            x => x.game.ToDto(
                relevantTeams[x.game.Team1Id],
                relevantTeams[x.game.Team2Id],
                x.SeasonId
            )
        );

        return gamesDto.ToList();
    }

    private async Task DatabaseValidations(GetTeamGamesInThisSeriesCommand command)
    {
        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();
        var team = await teamRepository.GetByIdAsync(command.TeamId);

        if (team is null)
            ThrowError("TeamId not found", StatusCodes.Status404NotFound);

        var seasonRepository = unitOfWork.Repository<Domain.Entities.Season>();
        var season = await seasonRepository.GetByIdAsync(command.SeasonId);

        if (season is null)
            ThrowError("SeasonId not found", StatusCodes.Status404NotFound);

        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();
        var series = await seriesRepository.GetByIdAsync(command.SeriesId);

        if (series is null)
            ThrowError("SeriesId not found", StatusCodes.Status404NotFound);

        if (series.SeasonId != command.SeasonId)
            ThrowError("SeriesId doesn't belong to SeasonId", StatusCodes.Status400BadRequest);

        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();

        var relevantTeamIds = gameRepository.Where(x => x.SeriesId == command.SeriesId)
            .Select(x => x.Team1Id)
            .Union(gameRepository.Where(x => x.SeriesId == command.SeriesId)
            .Select(x => x.Team2Id))
            .Distinct()
            .ToList();

        bool found = false;
        foreach (var relevantTeamId in relevantTeamIds)
        {
            if (relevantTeamId == command.TeamId)
                found = true;
        }

        if (!found)
            ThrowError("TeamId didn't play in SeriesId", StatusCodes.Status400BadRequest);
    }
}