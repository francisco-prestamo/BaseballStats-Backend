using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.ResultSets;

public class Alignment : PlayerPitcher
{
  
    public PlayerPositions Position {get; set;}

    public double Effectiveness {get; set;}
}