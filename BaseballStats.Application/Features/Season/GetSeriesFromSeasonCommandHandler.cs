using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.GetGamesFromSeries;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Season.GetSeriesFromSeason;

public class GetSeriesFromSeasonCommandHandler(IUnitOfWork unitOfWork): CommandHandler<GetSeriesFromSeasonCommand, List<SeriesDto>>
{
    public override async Task<List<SeriesDto>> ExecuteAsync(GetSeriesFromSeasonCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var seasonId = command.SeasonId;

        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();

        var series = seriesRepository.Where(x => x.SeasonId == seasonId).ToList();

        var seriesDto = series.Select(x => x.ToDto());

        return seriesDto.ToList();
    }

    private async Task DatabaseValidations(GetSeriesFromSeasonCommand command)
    {
        var seasonRepository = unitOfWork.Repository<Domain.Entities.Season>();
        var season = await seasonRepository.GetByIdAsync(command.SeasonId);

        if (season is null)
            ThrowError("SeasonId not found", StatusCodes.Status404NotFound);
    }
}
