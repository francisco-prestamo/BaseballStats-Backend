namespace BaseballStats.Application.DTOs;

public record RegisteredUserDto
{
    public string username { get; set; } = null!;

    public string? password {get; set;}

    public string userType {get; set;} = null!;
}