using BaseballStats.Application.Mappers;

namespace BaseballStats.Application.Utilities;

public static class PositionValidator
{
    public static bool IsValidPosition(this string position)
    {
        try
        {
            var playerPosition = position.GetPlayerPosition();
            return true;
        }
        catch
        {
            return false;
        }
    }
}