using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.ResultSets;

namespace BaseballStats.Application.Services;

public class AlignmentService(IUnitOfWork unitOfWork)
{
    public Task<(
        IEnumerable<Alignment> team1Alignment, 
        IEnumerable<Alignment> team2Alignment
        )> GetAlignmentsFromGame(long gameId)
    {
        var game = unitOfWork.Repository<Game>().DbSet;
        var alignedPlayerInGame = unitOfWork.Repository<AlignedPlayerInGame>().DbSet;
        var playerInPosition = unitOfWork.Repository<PlayerInPosition>().DbSet;
    } 

}