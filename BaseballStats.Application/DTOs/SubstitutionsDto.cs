using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.DTOs;

public class Substitution
{
    public long TeamId { get; set; }
    public PlayerInPosition PlayerIn { get; set; }
    public PlayerInPosition PlayerOut { get; set; }
    public TimeSpan Time { get; set; }

    public Substitution(long teamId, PlayerInPosition playerIn, PlayerInPosition playerOut, TimeSpan time)
    {
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