namespace BaseballStats.Domain.Interfaces.DataAccess;

public interface ITeamWithExtrasRepository
{
    /// <summary>
    /// Gets all teams with extra information.
    /// </summary>
    /// <returns>All teams with extra information.</returns>
    /// <remarks>
    /// The extra information includes the total runs, won games, and lost games.
    /// </remarks>
    IEnumerable<TeamWithExtras> GetTeamsWithExtras(); 
    /// <summary>
    /// Gets all teams with extra information that satisfy the specified predicate.
    /// </summary>
    /// <param name="predicate">A function to test each team with extra information for a condition.</param>
    /// <returns>All teams with extra information that satisfy the specified predicate.</returns>
    /// <remarks>
    /// The extra information includes the total runs, won games, and lost games.
    /// </remarks>
    IEnumerable<TeamWithExtras> GetTeamsWithExtras(Func<TeamWithExtras, bool> predicate);      
    
    /// <summary>
    /// Gets all teams with extra information that have at least one player in the specified series.
    /// </summary>
    /// <param name="SeriesId">The series identifier.</param>
    /// <returns>All teams with extra information that have at least one player in the specified series.</returns>
    /// <remarks>
    /// The extra information includes the total runs, won games, and lost games.
    /// </remarks>
    IEnumerable<TeamWithExtras> GetTeamsWithExtrasWithPlayerInSeries(long SeriesId);
}