using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.User.Delete;

public record DeleteUserCommand : ICommand<RegisteredUserDto>
{
    public long Id { get; init; }
}