using BaseballStats.Domain.Enums;

namespace BaseballStats.Domain.Entities;

public class TechnicalDirector : RegisteredUser
{
    public TechnicalDirector()
    {
        Type = UserTypes.TechnicalDirector;
    }
}