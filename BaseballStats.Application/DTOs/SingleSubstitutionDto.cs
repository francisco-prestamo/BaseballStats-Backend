namespace BaseballStats.Application.DTOs;

public class SingleSubstitutionDto
{
    public long Id {get; set;}
    public long TeamId { get; set; }
    public PlayerInPositionDto PlayerIn { get; set; } = null!;
    public PlayerInPositionDto PlayerOut { get; set; } = null!;
    public TimeSpan Time { get; set; }

}