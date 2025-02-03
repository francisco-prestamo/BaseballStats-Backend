using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.PlayerInSeries;

public class GetAllPlayerInSeriesTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllPlayerInSeries()
    {
        // Arrange
        var season = new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(1, 1000000000)
        };

        var series = new Domain.Entities.Series()
        {
            Id = Faker.Random.Long(1, 1000000000),
            SeasonId = season.Id,
            Name = Faker.Lorem.Word(),
            Type = Faker.Lorem.Word(),
            StartDate = Faker.Date.PastDateOnly(),
            EndDate = Faker.Date.FutureDateOnly()
        };

        var players = Enumerable.Range(1, 20).Select(_ => new Domain.Entities.Player()
        {
            Id = Faker.Random.Long(1, 1000000000)
        }).ToList();

        var playerInSeries = players.Select(x => new Domain.Entities.PlayerInSeries()
        {
            PlayerId = x.Id,
            SeriesId = series.Id
        }).ToList();

        var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        var seriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();
        var playerContext = DatabaseFixture.DbContext.Set<Domain.Entities.Player>();
        var playerInSeriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.PlayerInSeries>();

        await seasonContext.AddAsync(season);
        await seriesContext.AddAsync(series);
        await playerContext.AddRangeAsync(players);
        await playerInSeriesContext.AddRangeAsync(playerInSeries);

        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("/playerInSeries");
            var rspDto = await response.Content.ReadFromJsonAsync<List<PlayerInSeriesCRUDDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            playerInSeries.ForEach(x => rspDto.ShouldContain(y => y.PlayerId == x.PlayerId));
        }
        finally
        {
            // Clean up
            playerInSeriesContext.RemoveRange(playerInSeries);
            playerContext.RemoveRange(players);
            seriesContext.Remove(series);
            seasonContext.Remove(season);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}