namespace BaseballStats.Application.DTOs;

public record PitcherDto : RegularPlayerDto
{
    public int GamesWonNumber { get; set; }
    public int GamesLostNumber { get; set; }
    public bool RightHanded { get; set; }
    public double AllowedRunsAvg { get; set;}
}