using BaseballStats.Domain.Interfaces.DataAccess;

namespace BaseballStats.Application.Services;

public class ValidatePlayerInSeriesChangeService(IUnitOfWork unitOfWork)
{
    public (bool isValid, string errorMessage) CanUnassignPlayerFromCurrentTeamInSeries(long playerId, long seriesId)
    {
        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;
        var initialAlignments_table = unitOfWork.Repository<Domain.Entities.AlignedPlayerInGame>().DbSet;
        var game_table = unitOfWork.Repository<Domain.Entities.Game>().DbSet;
        var substitutions_table = unitOfWork.Repository<Domain.Entities.Substitution>().DbSet;
    
        var gamesInTheSeries = (
            from g in game_table
            where g.SeriesId == seriesId
            select g
        );

        var initialAlignments = (
            from ia in initialAlignments_table
            join g in gamesInTheSeries on ia.GameId equals g.Id
            select ia
        );

        var playerInInitialAlignmentGameId = (
            from ia in initialAlignments
            where ia.PlayerId == playerId
            select ia.GameId
        ).Cast<long?>().FirstOrDefault();

        if (playerInInitialAlignmentGameId != null)
            return (false, $"Player is in at least one initial alignment for currently assigned team in given series (e.g. in game with id {playerInInitialAlignmentGameId})");

        var playerSubstitutionGameId = (
            from s in substitutions_table
            join g in gamesInTheSeries on s.GameId equals g.Id
            where s.PlayerInId == playerId || s.PlayerOutId == playerId
            select s.GameId
        ).Cast<long?>().FirstOrDefault();

        if (playerSubstitutionGameId != null)
            return(false, $"Player is in at least one substitution for currently assigned team in given series (e.g. in game with id {playerSubstitutionGameId})");
    
        return (true, "");
    }
}