using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Application.ResultSets;
using BaseballStats.Application.Services;
using BaseballStats.Domain.Enums;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Features.Player.GetPlayerPositionsInSeries;

public class GetPlayerPositionsInSeriesCommandHandler(IUnitOfWork unitOfWork, SubstitutionService substitutionService) : CommandHandler<GetPlayerPositionsInSeriesCommand, List<PlayerInPositionDto>>
{
    public override async Task<List<PlayerInPositionDto>> ExecuteAsync(GetPlayerPositionsInSeriesCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;
        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;
        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;

        var teamId = 
            from pis in playerInSeries_table
            where pis.PlayerId == command.PlayerId && pis.SeriesId == command.SeriesId
            select pis.TeamId;
        
        if (teamId.IsNullOrEmpty()){ // player is not assigned to any team in the series
            return [];
        }

        var teamInitialAlignmentsAndSubstitutionsInSeries = await substitutionService.GetInitialAlignmemntsAndSubstitutionsForTeamInSeries((long)teamId.First()!, command.SeriesId);        
        var playedPositions = GetPlayedPositions(command.PlayerId, teamInitialAlignmentsAndSubstitutionsInSeries);


        var result = (
            from pp in playedPositions
            join pip in playerInPosition_table on new {command.PlayerId, Position = pp} equals new {pip.PlayerId, pip.Position}
            join p in player_table on pip.PlayerId equals p.Id
            select new Alignment
            {
                Id = p.Id,
                Name = p.Name,
                Age = p.Age,
                BattingAverage = p.BattingAverage,
                YearsOfExperience = p.YearsOfExperience,
                Effectiveness = pip.Effectiveness,
                Position = pp
            }
        ).Select(s => s.GetPlayerInPositionDto());

        return result.ToList();
    }

    private async Task DatabaseValidations(GetPlayerPositionsInSeriesCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
            ThrowError("PlayerId not found", StatusCodes.Status404NotFound);
    

        var seasonRepository = unitOfWork.Repository<Domain.Entities.Season>();
        var season = await seasonRepository.GetByIdAsync(command.SeasonId);

        if (season is null)
            ThrowError("SeasonId not found", StatusCodes.Status404NotFound);

        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();
        var series = await seriesRepository.GetByIdAsync(command.SeriesId);

        if (series is null)
            ThrowError("SeriesId not found", StatusCodes.Status404NotFound);

    }

    private List<PlayerPositions> GetPlayedPositions(long playerId, List<(long gameId, List<SubstitutionWithPosition> substitutions, List<InitialAlignment> initialAlignments)> allSubstitutions)
    {

        var playedPositions = (
            from p in allSubstitutions.Select(x => x.initialAlignments).SelectMany(x => x)
            group p by p.Position into g
            select g.Key
        ).ToList();

        
        playedPositions.AddRange(
            from s in allSubstitutions.Select(x => x.substitutions).SelectMany(x => x)
            where s.PlayerInId == playerId || s.PlayerOutId == playerId
            group s by s.Position into g
            select g.Key
        );

        playedPositions = (
            from p in playedPositions
            group p by p into g
            select g.Key
        ).ToList();



        return playedPositions;
    }

}