using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
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

        var games = gameRepository.Where(x => x.SeriesId == seriesId).ToList();

        var gamesDto = games.Select(x => x.ToDto());

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