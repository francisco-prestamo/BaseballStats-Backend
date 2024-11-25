using BaseballStats.Domain.Enums;
using BaseballStats.Domain.Interfaces.IEntity;

namespace BaseballStats.Domain.Entities.Identity;

public class RegisteredUser : IEntity
{
    public required long Id { get; set; } /* primary key */
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserTypes Type { get; set; }
}