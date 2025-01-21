using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Application.ResultSets;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Player.GetPlayerOrPitcher;

public class GetPlayerOrPitcherCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetPlayerOrPitcherCommand, PlayerOrPitcherDto>
{
    public override async Task<PlayerOrPitcherDto> ExecuteAsync(GetPlayerOrPitcherCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);
        
        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;
        var pitcher_table = unitOfWork.Repository<Domain.Entities.Pitcher>().DbSet;

        var playerPitchers =
            from p in player_table
            join pt in pitcher_table on p.Id equals pt.Id into playerOrPitcherTable
            from pt in playerOrPitcherTable.DefaultIfEmpty()
            where p.Id == command.Id
            select new PlayerPitcher
            {
                PlayerId = p.Id,
                PlayerName = p.Name,
                PlayerAge = p.Age,
                PlayerYearsOfExperience = p.YearsOfExperience,
                PlayerBattingAverage = p.BattingAverage,
                GamesWonNumber = pt.GamesWonNumber,
                GamesLostNumber = pt.GamesLostNumber,
                RightHanded = pt.RightHanded,
                AllowedRunsAvg = pt.AllowedRunsAvg
            };

        System.Console.WriteLine(string.Join('\n', playerPitchers.Select(x => x.AllowedRunsAvg).ToList()));

        return playerPitchers.Select(x => x.ToDto()).First();
    }

    private async Task DatabaseValidations(GetPlayerOrPitcherCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.Id);

        if (player is null)
            ThrowError("PlayerId not found", StatusCodes.Status404NotFound);
    }
}