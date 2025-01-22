using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.User.Put;

public record UpdateUserCommand : ICommand<RegisteredUserDto>
{
    public long Id { get; init; }
    public string Username { get; set; } = null!;
    public string? Password { get; set; }
    public string UserType { get; set; } = null!;

}