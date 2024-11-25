using BaseballStats.Domain.Enums;

namespace BaseballStats.Domain.Entities.Identity;

public class TechnicalDirector : RegisteredUser
{
    public TechnicalDirector()
    {
        Type = UserTypes.TechnicalDirector;
    }
}