using BaseballStats.Domain.Entities.Identity;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{
    public static List<RegisteredUser> GenerateUsers(int amount)
    {
        List<string> passwords = [
            "password123", "qwerty", "abc123", "letmein", "monkey", "dragon", "111111", "baseball", "iloveyou", "trustno1"
        ];

        var users = new List<RegisteredUser>();
        Random random = new Random(RandomSeed + 90);

        for (int i = 0; i < amount; i++)
        {
            var username = (FirstNames[random.Next(0, FirstNames.Count)] + '_' + LastNames[random.Next(0, LastNames.Count)]).ToLower();
            UserTypes userType = random.Next(0, 2) switch{
                0 => UserTypes.Admin,
                1 => UserTypes.TechnicalDirector,
                _ => UserTypes.Journalist
            };

            var user = new RegisteredUser
            {
                Id = random.NextInt64(),
                Username = username,
                Password = passwords[random.Next(0, passwords.Count)],
                Type = userType
            };
            users.Add(user);
        }

        users.Add(new RegisteredUser()
        {
            Id = random.NextInt64(),
            Username = "admin",
            Password = "admin",
            Type = UserTypes.Admin
        });

        return users;
    }


}
