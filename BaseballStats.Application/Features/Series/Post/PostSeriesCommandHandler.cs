using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Series.Post;

public class PostSeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostSeriesCommand, SeriesDto>
{
    public override async Task<SeriesDto> ExecuteAsync(PostSeriesCommand command, CancellationToken ct = default)
    {
        await DatabaseValidation(command);

        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();

        var series = new Domain.Entities.Series
        {
            Name = command.Name,
            Type = command.Type,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            SeasonId = command.SeasonId
        };

        series = await seriesRepository.AddAsync(series);

        await unitOfWork.SaveChangesAsync(ct);

        return series.ToDto();
    }

    private async Task DatabaseValidation(PostSeriesCommand command)
    {
        var season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.SeasonId);
        if (season == null)
            ThrowError("Season Id not found.", StatusCodes.Status400BadRequest);
    }
}