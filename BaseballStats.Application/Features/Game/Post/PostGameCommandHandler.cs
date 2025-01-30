using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Game.Post;

public class PostGameCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostGameCommand, GameDto>
{
    public override async Task<GameDto> ExecuteAsync(PostGameCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidations(command);

        var repository = unitOfWork.Repository<Domain.Entities.Game>();

        var game = new Domain.Entities.Game
        {
            Team1Id = command.Team1Id,
            Team2Id = command.Team2Id,
            Date = command.Date,
            Winner1 = command.WinTeam,
            Runs1 = command.Team1Runs,
            Runs2 = command.Team2Runs,
            SeriesId = command.SeriesId,
        };

        await repository.AddAsync(game);
        await unitOfWork.SaveChangesAsync(ct);

        return new GameDto
        {
            Id = game.Id,
            Team1Id = game.Team1Id,
            Team2Id = game.Team2Id,
            Date = game.Date,
            WinTeam = game.Winner1,
            Team1Runs = game.Runs1,
            Team2Runs = game.Runs2,
            SeriesId = game.SeriesId,
        };
    }

    private async Task DatabaseValidations(PostGameCommand command)
    {
        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();
        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();

        var team1 = await teamRepository.GetByIdAsync(command.Team1Id);

        if (team1 is null)
            ThrowError("Team1Id does not exist", StatusCodes.Status400BadRequest);

        var team2 = await teamRepository.GetByIdAsync(command.Team2Id);

        if (team2 is null)
            ThrowError("Team2Id does not exist", StatusCodes.Status400BadRequest);

        var series = await seriesRepository.GetByIdAsync(command.SeriesId);

        if (series is null)
            ThrowError("SeriesId does not exist", StatusCodes.Status400BadRequest);
        
        var game = await gameRepository.FirstOrDefaultAsync(x => x.Team1Id == command.Team1Id && x.Team2Id == command.Team2Id && x.Date == command.Date);
        
        if(game is not null)
            ThrowError("There is already a game between those teams on that day", StatusCodes.Status400BadRequest);
    }
}