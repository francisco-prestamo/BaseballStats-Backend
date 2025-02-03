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
            Id = command.Id,
            Name = command.Name,
            Type = command.Type,
            StartDate = new DateOnly(command.StartDate.Year, command.StartDate.Month, command.StartDate.Day),
            EndDate = new DateOnly(command.EndDate.Year, command.EndDate.Month, command.EndDate.Day),
            SeasonId = command.IdSeason
        };

        series = await seriesRepository.AddAsync(series);

        await unitOfWork.SaveChangesAsync(ct);

        return series.ToDto();
    }

    private async Task DatabaseValidation(PostSeriesCommand command)
    {
        var series = await unitOfWork.Repository<Domain.Entities.Series>().GetByIdAsync(command.Id);
        if (series != null)
            ThrowError("Series Id already exists.", StatusCodes.Status409Conflict);

        var season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.IdSeason);
        if (season == null)
            ThrowError("Season Id not found.", StatusCodes.Status400BadRequest);
    }
}