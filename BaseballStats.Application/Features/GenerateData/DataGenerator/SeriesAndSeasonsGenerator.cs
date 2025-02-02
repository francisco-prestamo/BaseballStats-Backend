namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{

    private static (List<Domain.Entities.Series> series, List<Domain.Entities.Season> seasons) GenerateSeriesAndSeasons(int AmountSeasons, int AmountSeriesPerSeason)
    {
        List<string> seasonsOfTheYear = [
            "Spring", "Summer", "Fall", "Winter"
        ];
        List<string> Locality = [
            "Local", "Regional", "National", "International"
        ];
        List<string> CompetitionNames = [
            "Championship", "Cup", "Tournament", "League", "Series"
        ];

        var random = new Random(RandomSeed + 77);
        var seasons = Enumerable.Range(1, AmountSeasons).Select(x => new Domain.Entities.Season { Id = x }).ToList();
        var series = new List<Domain.Entities.Series>();
        
        var beginDate = DateTime.Now.Subtract(TimeSpan.FromDays(365*3));
        for (int i = 0; i < AmountSeasons; i++)
        {
            for (int j = 0; j < AmountSeriesPerSeason; j++)
            {
                var seasonId = i + 1;
                var name = seasonsOfTheYear[random.Next(0, seasonsOfTheYear.Count)] + ' ' + Locality[random.Next(0, Locality.Count)] + ' ' + CompetitionNames[random.Next(0, CompetitionNames.Count)];
                var startDate = beginDate.Add(TimeSpan.FromDays(random.Next(365)));
                var endDate = startDate.Add(TimeSpan.FromDays(random.Next(1, 30)));
                var seriesObj = new Domain.Entities.Series
                {
                    Name = name,
                    SeasonId = seasonId,
                    StartDate = DateOnly.FromDateTime(startDate),
                    EndDate = DateOnly.FromDateTime(endDate)
                };

                series.Add(seriesObj);
            }
        }

        return (series, seasons);
    }


}