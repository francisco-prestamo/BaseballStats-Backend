namespace BaseballStats.Application.DTOs;

public record RegisteredUserDto
{
    public string Username { get; set; } = null!;

    public string? Password {get; set;}

    public string UserType {get; set;} = null!;
}