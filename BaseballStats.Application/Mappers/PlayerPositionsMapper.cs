using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.Mappers;

public static class PlayerPositionsMapper
{
    public static string GetDisplayName(this PlayerPositions position)
    {   
        return position switch
        {
            PlayerPositions.Pitcher => "Pitcher",
            PlayerPositions.Catcher => "Catcher",
            PlayerPositions.FirstBase => "First-Base",
            PlayerPositions.SecondBase => "Second-Base",
            PlayerPositions.ThirdBase => "Third-Base",
            PlayerPositions.ShortStop => "Shortstop",
            PlayerPositions.LeftField => "Left-Field",
            PlayerPositions.CenterField => "Center-Field",
            PlayerPositions.RightField => "Right-Field",
            PlayerPositions.DesignatedHitter => "Designated-Hitter",
            _ => "Unknown"
        };
    }

    public static PlayerPositions GetPlayerPosition(this string position)
    {
        return position switch
        {
            "Pitcher" => PlayerPositions.Pitcher,
            "Catcher" => PlayerPositions.Catcher,
            "First-Base" => PlayerPositions.FirstBase,
            "Second-Base" => PlayerPositions.SecondBase,
            "Third-Base" => PlayerPositions.ThirdBase,
            "Shortstop" => PlayerPositions.ShortStop,
            "Left-Field" => PlayerPositions.LeftField,
            "Center-Field" => PlayerPositions.CenterField,
            "Right-Field" => PlayerPositions.RightField,
            "Designated-Hitter" => PlayerPositions.DesignatedHitter,
            _ => throw new ArgumentException("Invalid position")
        };
    }
}