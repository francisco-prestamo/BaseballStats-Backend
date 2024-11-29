using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Auth;

public record RegisterUserCommand : ICommand<RegisterUserResponse>
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Role { get; init; } = null!;
}