using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Application.ResultSets;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Player.GetPlayerPositionsInSeries;

public class GetPlayerPositionsInSeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetPlayerPositionsInSeriesCommand, List<PlayerInPositionDto>>
{
    public override async Task<List<PlayerInPositionDto>> ExecuteAsync(GetPlayerPositionsInSeriesCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;
        var alignedPlayerInGame_table = unitOfWork.Repository<Domain.Entities.AlignedPlayerInGame>().DbSet;
        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;
        var game_table = unitOfWork.Repository<Domain.Entities.Game>().DbSet;

        var gamesInThisSeries =
            from g in game_table
            where g.SeriesId == command.SeriesId
            select g.Id;
        
        var positionsPlayed =
            from apig in alignedPlayerInGame_table
            join gits in gamesInThisSeries on apig.GameId equals gits
            join pip in playerInPosition_table on new { apig.PlayerId, apig.Position } equals new { pip.PlayerId, pip.Position }
            join p in player_table on apig.PlayerId equals p.Id
            where apig.PlayerId == command.PlayerId
            select new Alignment
            {
                Id = apig.PlayerId,
                BattingAverage = p.BattingAverage,
                Name = p.Name,
                Age = p.Age,
                YearsOfExperience = p.YearsOfExperience,
                Position = apig.Position,
                Effectiveness = pip.Effectiveness
            };

        return positionsPlayed.Select(x => x.GetPlayerInPositionDto()).ToList();
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
}