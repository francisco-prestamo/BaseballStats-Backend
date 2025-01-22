using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.DTOs;

public class PlayerInPositionDto
{
    public RegularPlayerDto Player {get; set;} = null!;

    public string Position {get; set;} = null!;

    public double Efectividad {get; set;}
    
}