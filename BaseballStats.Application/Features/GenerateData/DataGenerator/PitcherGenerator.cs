namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{
    private static List<Domain.Entities.Pitcher> GeneratePitchers(List<long> playerIds)
    {
        var random = new Random(RandomSeed);

        var answ = new List<Domain.Entities.Pitcher>();

        foreach(var id in playerIds)
        {
            answ.Add(new() {
                Id = id,
                AllowedRunsAvg = random.NextDouble(),
                GamesWonNumber = random.Next(0, 501),
                GamesLostNumber = random.Next(0, 501),
                RightHanded = random.Next(2) == 1
            });
        }

        return answ;
    }
}