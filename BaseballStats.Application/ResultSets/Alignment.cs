using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.ResultSets;

public class Alignment : Player
{
  
    public PlayerPositions Position {get; set;}

    public double Effectiveness {get; set;}
}