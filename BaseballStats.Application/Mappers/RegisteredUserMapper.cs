using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Entities.Identity;

namespace BaseballStats.Application.Mappers;

public static class RegisteredUserMapper
{
    public static RegisteredUserDto ToDto(this RegisteredUser user)
    {
        return new RegisteredUserDto()
        {
           username = user.Username,
           userType = user.Type.ToString()
        };
    }
}