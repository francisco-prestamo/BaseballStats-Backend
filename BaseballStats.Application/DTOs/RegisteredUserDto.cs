namespace BaseballStats.Application.DTOs;

public record RegisteredUserDto
{
    public long Id { get; init; }
    public string Username { get; set; } = null!;

    public string? Password {get; set;}

    public string UserType {get; set;} = null!;

    public string? Token {get; set;}
}