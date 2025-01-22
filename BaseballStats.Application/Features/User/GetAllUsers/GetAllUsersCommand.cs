using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.User.GetAllUsers
{
    public record GetAllUsersCommand : ICommand<List<RegisteredUserDto>>
    {
    }
}