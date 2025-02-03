using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.Season;

public class PutSeasonTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task PutSeasonForbidden()
    {
        // Arrange
        var season = new SeasonDto()
        {
            Id = Faker.Random.Long(4001000, 4002000)
        };

        // Act
        var response = await Admin.PutAsync($"seasons/{season.Id}", JsonContent.Create(season));

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
    }
}