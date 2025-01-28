using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Game.Put;

public class PutGameCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PutGameCommand, GameDto>
{
    public override async Task<GameDto> ExecuteAsync(PutGameCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidations(command);

        var repository = unitOfWork.Repository<Domain.Entities.Game>();

        var game = await repository.GetByIdAsync(command.Id);
        var series = (await unitOfWork.Repository<Domain.Entities.Series>().GetByIdAsync(command.SeriesId))!;

        game!.Team1Id = command.Team1Id;
        game.Team2Id = command.Team2Id;
        game.Date = command.Date;
        game.Winner1 = command.WinTeam;
        game.Runs1 = command.Team1Runs;
        game.Runs2 = command.Team2Runs;
        game.SeriesId = command.SeriesId;

        await unitOfWork.SaveChangesAsync(ct);

        return game.ToGameDto(series.SeasonId);
    }

    private async Task DatabaseValidations(PutGameCommand command)
    {
        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();
        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();

        var game = await gameRepository.GetByIdAsync(command.Id);

        if (game is null)
            ThrowError("Game not found", StatusCodes.Status404NotFound);

        var team1 = await teamRepository.GetByIdAsync(command.Team1Id);

        if (team1 is null)
            ThrowError("Team1Id does not exist", StatusCodes.Status400BadRequest);

        var team2 = await teamRepository.GetByIdAsync(command.Team2Id);

        if (team2 is null)
            ThrowError("Team2Id does not exist", StatusCodes.Status400BadRequest);

        var series = await seriesRepository.GetByIdAsync(command.SeriesId);

        if (series is null)
            ThrowError("SeriesId does not exist", StatusCodes.Status400BadRequest);
    }
}