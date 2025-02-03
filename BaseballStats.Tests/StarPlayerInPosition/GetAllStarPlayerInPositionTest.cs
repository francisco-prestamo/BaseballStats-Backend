using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Enums;
using Microsoft.OpenApi.Extensions;

namespace BaseballStats.Tests.StarPlayerInPosition;

public class GetAllStarPlayerInPositionTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllStarPlayerInPosition()
    {
        // Arrange

        var season = new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(10000, 1000000)
        };

        var series = new Domain.Entities.Series()
        {
            Id = Faker.Random.Long(10000, 1000000),
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

        var playersInPosition = players.Select(x => new Domain.Entities.PlayerInPosition()
        {
            PlayerId = x.Id,
            Position = (PlayerPositions)Faker.Random.Int(0, Enum.GetValues<PlayerPositions>().Length - 1),
            Effectiveness = Faker.Random.Double()
        }).ToList();

        var starPlayerInPosition = playersInPosition.Select(x => new Domain.Entities.StarPlayerInPosition()
        {
            PlayerId = x.PlayerId,
            Position = x.Position,
            SeriesId = series.Id
        }).ToList();

        var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        var seriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();
        var playerContext = DatabaseFixture.DbContext.Set<Domain.Entities.Player>();
        var playerInPositionContext = DatabaseFixture.DbContext.Set<Domain.Entities.PlayerInPosition>();
        var starPlayerInPositionContext = DatabaseFixture.DbContext.Set<Domain.Entities.StarPlayerInPosition>();

        await seasonContext.AddAsync(season);
        await seriesContext.AddAsync(series);
        await playerContext.AddRangeAsync(players);
        await playerInPositionContext.AddRangeAsync(playersInPosition);
        await starPlayerInPositionContext.AddRangeAsync(starPlayerInPosition);

        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("starPlayerInPosition");
            var rspDto = await response.Content.ReadFromJsonAsync<List<StarPlayerInPositionDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            starPlayerInPosition.ForEach(x => rspDto.ShouldContain(y =>
                y.PlayerId == x.PlayerId &&
                y.SeriesId == x.SeriesId &&
                y.SeasonId == season.Id
            ));
        }
        finally
        {
            // Clean up
            starPlayerInPositionContext.RemoveRange(starPlayerInPosition);
            playerInPositionContext.RemoveRange(playersInPosition);
            playerContext.RemoveRange(players);
            seriesContext.Remove(series);
            seasonContext.Remove(season);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}