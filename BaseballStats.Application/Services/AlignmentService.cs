using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.ResultSets;

namespace BaseballStats.Application.Services;

public class AlignmentService(IUnitOfWork unitOfWork)
{
    public Task<List<Alignment>> GetAlignmentsFromGame(long gameId, long teamId)
    {
        var alignedPlayerInGame_table = unitOfWork.Repository<AlignedPlayerInGame>().DbSet;
        var playerInPosition_table = unitOfWork.Repository<PlayerInPosition>().DbSet;
        var player_table = unitOfWork.Repository<Player>().DbSet;
        var pitcher_table = unitOfWork.Repository<Pitcher>().DbSet;

        var result = 
            from alignedPlayerInGame in alignedPlayerInGame_table
            join player in player_table on alignedPlayerInGame.PlayerId equals player.Id
            join playerInPosition in playerInPosition_table on 
                new {alignedPlayerInGame.PlayerId, alignedPlayerInGame.Position} equals 
                new {playerInPosition.PlayerId, playerInPosition.Position}
            where alignedPlayerInGame.GameId == gameId && alignedPlayerInGame.TeamId == teamId
            select new Alignment
            {
                Id = player.Id,
                Name = player.Name,
                Age = player.Age,
                BattingAverage = player.BattingAverage,
                YearsOfExperience = player.YearsOfExperience,
                Effectiveness = playerInPosition.Effectiveness,
                Position = alignedPlayerInGame.Position
            };

        return Task.FromResult(result.ToList());    
    }
}