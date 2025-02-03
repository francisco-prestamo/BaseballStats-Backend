namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{
    public static List<Domain.Entities.Game> GenerateGames(List<Domain.Entities.Series> series, List<Domain.Entities.Team> teams, List<Domain.Entities.PlayerInSeries> playerInSeries)
    {
        var random = new Random(RandomSeed + 30);

        var answ = new List<Domain.Entities.Game>();


        foreach(var s in series)
        {
            var relevantTeams = (
                from pis in playerInSeries
                where pis.SeriesId == s.Id
                select pis.TeamId!.Value
            ).Distinct().ToList();

            var (games, newDate, winner) = SimulateGroupStage(relevantTeams, s.Id, s.StartDate, ref random);

            answ.AddRange(games);
        }

        return answ;

    }

    private static (List<Domain.Entities.Game> games, DateOnly newDate, long winnerId) SimulateGroupStage(List<long> teams, long SeriesId, DateOnly date, ref Random random)
    {
        System.Console.WriteLine($"Simulating group stage for {teams.Count} teams");

        if(teams.Count <= 0)
            throw new ArgumentException("Invalid number of teams");

        if (teams.Count == 1)
            return ([], date, teams[0]);

        var (leftGames, newDate1, winner1) = SimulateGroupStage(teams.Take(teams.Count  / 2).ToList(), SeriesId, date, ref random);
        var (rightGames, newDate2, winner2) = SimulateGroupStage(teams.Skip(teams.Count / 2).ToList(), SeriesId, date, ref random);

        var games = new List<Domain.Entities.Game>();

        games.AddRange(leftGames);
        games.AddRange(rightGames);

        var newStartDate = newDate1 > newDate2 ? newDate1 : newDate2;
        newStartDate = newStartDate.AddDays(2);

        int winCount = 0;

        for (int i = 0; i < 5; i++)
        {
            var runs1 = random.Next(0, 10);
            var runs2 = random.Next(0, 10);

            if (runs1 == runs2)
            {
                runs1++;
            }

            var game = new Domain.Entities.Game
            {
                Id = random.NextInt64(),
                SeriesId = SeriesId,
                Team1Id = winner1,
                Team2Id = winner2,
                Date = newStartDate,
                Runs1 = runs1,
                Runs2 = runs2
            };

            if (runs1 > runs2)
            {
                winCount++;
            }
            else {
                winCount--;
            }

            games.Add(game);

            newStartDate = newStartDate.AddDays(2);
        }

        var winner = winCount > 0 ? winner1 : winner2;

        return (games, newStartDate, winner);

    }    
}
