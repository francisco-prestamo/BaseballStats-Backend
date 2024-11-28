using BaseballStats.Domain.Enums;

namespace BaseballStats.Domain.Entities.Identity;

public class RegisteredUser : Entity<long>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserTypes Type { get; set; }
}