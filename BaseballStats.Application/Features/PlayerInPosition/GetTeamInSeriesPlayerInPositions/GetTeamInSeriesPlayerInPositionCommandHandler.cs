using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.PlayerInPosition.GetTeamInSeriesPlayerInPositions;

public class GetTeamInSeriesPlayerInPositionsCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetTeamInSeriesPlayerInPositionsCommand, List<PlayerInPositionCRUDDto>>
{
    public override Task<List<PlayerInPositionCRUDDto>> ExecuteAsync(GetTeamInSeriesPlayerInPositionsCommand command, CancellationToken ct = default)
    {
        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;
        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;

        var result = (
            from pis in playerInSeries_table
            join pip in playerInPosition_table on pis.PlayerId equals pip.PlayerId
            where pis.TeamId == command.TeamId && pis.SeriesId == command.SeriesId
            select new PlayerInPositionCRUDDto()
            {
                PlayerId = pis.PlayerId,
                Position = pip.Position.GetDisplayName(),
                Effectiveness = pip.Effectiveness
            }
        ).ToList();

        return Task.FromResult(result);
    }

    private async Task DatabaseValidation(GetTeamInSeriesPlayerInPositionsCommand command)
    {
        var series = await unitOfWork.Repository<Domain.Entities.Series>().GetByIdAsync(command.SeriesId);
        var season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.SeasonId);
        var team = await unitOfWork.Repository<Domain.Entities.Team>().GetByIdAsync(command.TeamId);

        if (series == null)
            ThrowError("Series not found", StatusCodes.Status404NotFound);

        if (season == null)
            ThrowError("Season not found", StatusCodes.Status404NotFound);

        if (series.SeasonId != season.Id)
            ThrowError("Series not found", StatusCodes.Status400BadRequest);

        if (team == null)
            ThrowError("Team not found", StatusCodes.Status404NotFound);
    }
}