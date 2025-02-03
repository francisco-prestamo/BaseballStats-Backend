using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Entities.Identity;

namespace BaseballStats.Tests.Team;

public class GetTeamGamesInThisSeriesTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetTeamGamesInThisSeriesSuccess()
    {
        // Arrange
        var technicalDirector = new Domain.Entities.Identity.TechnicalDirector()
        {
            Id = Faker.Random.Long(1, 1000000000)
        };

        var team1 = new Domain.Entities.Team()
        {
            Id = Faker.Random.Long(1, 1000000000),
            Name = Faker.Lorem.Word(),
            Color = Faker.Lorem.Word(),
            Initials = "ASD",
            TechnicalDirectorId = technicalDirector.Id,
            RepresentedEntity = Faker.Lorem.Word()
        };

        var team2 = new Domain.Entities.Team()
        {
            Id = Faker.Random.Long(1, 1000000000),
            Name = Faker.Lorem.Word(),
            Color = Faker.Lorem.Word(),
            Initials = "EFG",
            TechnicalDirectorId = technicalDirector.Id,
            RepresentedEntity = Faker.Lorem.Word()
        };

        var season = new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(1, 1000000000)
        };

        var series = new Domain.Entities.Series()
        {
            Id = Faker.Random.Long(1, 1000000000),
            SeasonId = season.Id
        };

        var games = Enumerable.Range(1, 5).Select(_ => new Game()
        {
            Id = Faker.Random.Long(1, 1000000000),
            SeriesId = series.Id,
            Team1Id = team1.Id,
            Team2Id = team2.Id,
            Date = Faker.Date.PastDateOnly(),
        }).ToList();

        var technicalDirectorContext = DatabaseFixture.DbContext.Set<Domain.Entities.Identity.TechnicalDirector>();
        var teamContext = DatabaseFixture.DbContext.Set<Domain.Entities.Team>();
        var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        var seriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();
        var gameContext = DatabaseFixture.DbContext.Set<Game>();

        await technicalDirectorContext.AddAsync(technicalDirector);
        await teamContext.AddAsync(team1);
        await teamContext.AddAsync(team2);
        await seasonContext.AddAsync(season);
        await seriesContext.AddAsync(series);
        await gameContext.AddRangeAsync(games);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync($"teams/{team1.Id}/serie/{season.Id}/{series.Id}/games");
            var rspDto = await response.Content.ReadFromJsonAsync<List<GameWithTeamsDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            games.ForEach(x => rspDto.ShouldContain(y => y.Id == x.Id && y.Team1.Id == x.Team1Id && y.Team2.Id == x.Team2Id));
        }
        finally
        {
            // Clean up
            gameContext.RemoveRange(games);
            seriesContext.Remove(series);
            seasonContext.Remove(season);
            teamContext.Remove(team1);
            teamContext.Remove(team2);
            technicalDirectorContext.Remove(technicalDirector);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}