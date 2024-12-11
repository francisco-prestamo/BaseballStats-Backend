using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BaseballStats.Domain.Interfaces.DataAccess;

namespace Infrastructure.Data.DataAccess;

public class TeamWithExtrasRepository(AppDbContext _context) : ITeamWithExtrasRepository
{
    const string TeamWithExtrasQuery = @"
        SELECT 
            t.*,
            SUM(
                CASE WHEN t.""Id"" = g.""Team1Id"" THEN g.""Runs1""
                ELSE g.""Runs2""
                END
            ) ""totalRuns"",
            SUM(
                CASE 
                WHEN t.""Id"" = g.""Team1Id"" THEN 
                    (CASE WHEN g.""Winner1"" THEN 1 ELSE 0 END)
                ELSE 
                    (CASE WHEN g.""Winner1"" THEN 0 ELSE 1 END)
                END
            ) ""winGames"",
            SUM(
                CASE WHEN t.""Id"" = g.""Team1Id"" THEN 
                    (CASE WHEN g.""Winner1"" THEN 0 ELSE 1 END)
                ELSE 
                    (CASE WHEN g.""Winner1"" THEN 1 ELSE 0 END)
                END
            ) ""loseGames""
        FROM ""Team"" t
            JOIN ""Game"" g ON 
                g.""Team1Id"" = t.""Id"" OR
                g.""Team2Id"" = t.""Id""
        GROUP BY t.""Id""
        "
    ;

    public IEnumerable<TeamWithExtras> GetTeamsWithExtras()
    {
        return this.GetTeamsWithExtras(x => true);
    }
    
    public IEnumerable<TeamWithExtras> GetTeamsWithExtras(Func<TeamWithExtras, bool> predicate)
    {
        return _context.Database.SqlQueryRaw<TeamWithExtras>(TeamWithExtrasQuery).Where(predicate);
    }

    public IEnumerable<TeamWithExtras> GetTeamsWithExtrasWithPlayerInSeries(long SeriesId)
    {
        
        #pragma warning disable 
        // this warns us about possible sql injection with SeriesId, but it is a long and not a string
        return _context.Database.SqlQueryRaw<TeamWithExtras>($@"
            SELECT *
            FROM ({TeamWithExtrasQuery}) twe
            WHERE twe.""Id"" IN (
                    SELECT DISTINCT ""TeamId""
                    FROM ""PlayerInSeries"" pis
                    WHERE pis.""SeriesId"" = {SeriesId}
                )
        ");
        #pragma warning restore
    }
}