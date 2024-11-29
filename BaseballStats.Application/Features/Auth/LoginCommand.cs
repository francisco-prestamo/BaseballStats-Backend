using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Auth;

public record LoginCommand : ICommand<RegisteredUserDto>
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}