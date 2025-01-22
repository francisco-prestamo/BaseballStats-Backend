using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.Mappers;

public static class UserTypeMapper
{
    public static string GetDisplayName(this UserTypes userType)
    {
        return userType switch
        {
            UserTypes.Admin => "admin",
            UserTypes.TechnicalDirector => "dt",
            UserTypes.Journalist => "journalist",
            _ => throw new ArgumentOutOfRangeException(nameof(userType), userType, null)
        };
    }

    public static UserTypes ToUserType(this string userType)
    {
        return userType switch
        {
            "admin" => UserTypes.Admin,
            "dt" => UserTypes.TechnicalDirector,
            "journalist" => UserTypes.Journalist,
            _ => throw new ArgumentOutOfRangeException(nameof(userType), userType, null)
        };
    }
}