namespace BaseballStats.Application.DTOs;

public class Substitution {
    public long TeamId { get; set; }
    public long PlayerIn { get; set; }
    public long PlayerOut { get; set; }
    public TimeSpan Time { get; set; }
    
    public Substitution(long teamId, long playerIn, long playerOut, TimeSpan time) {
        TeamId = teamId;
        PlayerIn = playerIn;
        PlayerOut = playerOut;
        Time = time;
    }
}

public record SubstitutionsDto
{
    public List<Substitution> Team1Substitutions { get; init; } = null!;
    public List<Substitution> Team2Substitutions { get; init; } = null!;
}