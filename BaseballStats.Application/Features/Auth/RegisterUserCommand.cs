using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Auth;

public record RegisterUserCommand : ICommand<RegisteredUserDto>
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string UserType { get; init; } = null!;
}