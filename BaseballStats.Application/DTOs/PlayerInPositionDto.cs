using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.DTOs;

public class PlayerInPositionDto
{
    public PlayerDto Player {get; set;} = null!;

    public PlayerPositions Position {get; set;}

    public double Effectiveness {get; set;}
    
}