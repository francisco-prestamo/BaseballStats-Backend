using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.Game;

public class GetAllGamesTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllGames()
    {
        // Arrange
        var season = new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(1, 1000000000)
        };

        var serie = new Domain.Entities.Series()
        {
            Id = Faker.Random.Long(1, 1000000000),
            SeasonId = season.Id,
            Name = Faker.Lorem.Word(),
            Type = Faker.Lorem.Word(),
            StartDate = Faker.Date.PastDateOnly(),
            EndDate = Faker.Date.PastDateOnly()
        };
        
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

        var games = Enumerable.Range(1, 20).Select(_ => new Domain.Entities.Game()
        {
            Id = Faker.Random.Long(1, 1000000000),
            SeriesId = serie.Id,
            Team1Id = team1.Id,
            Team2Id = team2.Id,
            Date = Faker.Date.PastDateOnly(),
            Runs1 = 10,
            Runs2 = 5,
            Winner1 = true
        }).ToList();

        var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        var serieContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();
        var technicalDirectorContext = DatabaseFixture.DbContext.Set<Domain.Entities.Identity.TechnicalDirector>();
        var teamContext = DatabaseFixture.DbContext.Set<Domain.Entities.Team>();
        var gameContext = DatabaseFixture.DbContext.Set<Domain.Entities.Game>();

        await seasonContext.AddAsync(season);
        await serieContext.AddAsync(serie);
        await technicalDirectorContext.AddAsync(technicalDirector);
        await teamContext.AddAsync(team1);
        await teamContext.AddAsync(team2);
        await gameContext.AddRangeAsync(games);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("games");
            var rspDto = await response.Content.ReadFromJsonAsync<List<GameDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            games.ForEach(x => rspDto.ShouldContain(y => y.Id == x.Id));
        }
        finally
        {
            // Clean up

            gameContext.RemoveRange(games);
            teamContext.Remove(team2);
            teamContext.Remove(team1);
            technicalDirectorContext.Remove(technicalDirector);
            serieContext.Remove(serie);
            seasonContext.Remove(season);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}