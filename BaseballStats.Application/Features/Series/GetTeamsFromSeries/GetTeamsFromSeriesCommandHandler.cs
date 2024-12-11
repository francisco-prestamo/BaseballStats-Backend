using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Domain.Interfaces.IEntity;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Series.GetTeamsFromSeries;

public class GetTeamsFromSeriesCommandHandler(ITeamWithExtrasRepository teamWithExtrasRepository, IUnitOfWork unitOfWork) : CommandHandler<GetTeamsFromSeriesCommand, List<TeamWithExtrasDto>>
{

    public override async Task<List<TeamWithExtrasDto>> ExecuteAsync(GetTeamsFromSeriesCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var seriesId = command.SeriesId;

        // teams with at least one player in the series
        var teams = teamWithExtrasRepository.GetTeamsWithExtrasWithPlayerInSeries(seriesId);

        var teamWithExtrasDtos = teams.Select(x => x.ToDto());
        return teamWithExtrasDtos.ToList();
    }

    private async Task DatabaseValidations(GetTeamsFromSeriesCommand command)
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