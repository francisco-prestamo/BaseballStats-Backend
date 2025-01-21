namespace BaseballStats.Application.ResultSets;

public class PlayerPitcher
{
    public long PlayerId {get; set;}
    public string PlayerName {get; set;} = null!;
    public int PlayerAge {get; set;}
    public int PlayerYearsOfExperience {get; set;}
    public double? PlayerBattingAverage {get; set;}
    
    public int? GamesWonNumber {get; set;}
    public int? GamesLostNumber {get; set;}
    public bool? RightHanded {get; set;}
    public double? AllowedRunsAvg {get; set;}
}