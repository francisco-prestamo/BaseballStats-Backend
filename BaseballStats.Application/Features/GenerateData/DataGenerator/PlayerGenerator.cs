namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{
    private static List<Domain.Entities.Player> GeneratePlayers(int Amount)
    {
        var random = new Random(RandomSeed + 1);
        var players = new List<Domain.Entities.Player>();
        for (int i = 0; i < Amount; i++)
        {
            var name = FirstNames[random.Next(0, FirstNames.Count)] + ' ' + LastNames[random.Next(0, LastNames.Count)];
            var age = random.Next(18, 40);
            var yearsOfExperience = random.Next(0, age - 18);
            double? battingAverage = random.Next(10) < 2 ? null : random.NextDouble() * (0.4 - 0.1) + 0.1;

            var player = new Domain.Entities.Player
            {
                Name = name,
                Age = age,
                YearsOfExperience = yearsOfExperience,
                BattingAverage = battingAverage
            };
            players.Add(player);
        }

        return players;
    }
}