using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.GetAll;

public class GetAllGamesCommandHadler(IUnitOfWork unitOfWork) : CommandHandler<GetAllGamesCommand, List<GameDto>>
{
    public override async Task<List<GameDto>> ExecuteAsync(GetAllGamesCommand command, CancellationToken ct = default)
    {
        var game_table = unitOfWork.Repository<Domain.Entities.Game>().DbSet;
        var series_table = unitOfWork.Repository<Domain.Entities.Series>().DbSet;

        var games = (
            from game in game_table
            join series in series_table on game.SeriesId equals series.Id
            select game.ToGameDto(series.SeasonId)
        ).ToList();

        await Task.CompletedTask;

        return games;
    } 

}