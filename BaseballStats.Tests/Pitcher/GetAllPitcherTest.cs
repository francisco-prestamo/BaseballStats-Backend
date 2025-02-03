using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.Pitcher;

public class GetAllPitcherTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllPitcher()
    {
        // Arrange

        var pitchers = Enumerable.Range(1, 20).Select(_ => new Domain.Entities.Pitcher()
        {
            Id = Faker.Random.Long(1, 1000000000)
        }).ToList();

        var players = pitchers.Select(x => new Domain.Entities.Player() { Id = x.Id }).ToList();

        var playersContext = DatabaseFixture.DbContext.Set<Domain.Entities.Player>();
        var pitchersContext = DatabaseFixture.DbContext.Set<Domain.Entities.Pitcher>();

        await playersContext.AddRangeAsync(players);
        await pitchersContext.AddRangeAsync(pitchers);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("pitchers/");
            var rspDto = await response.Content.ReadFromJsonAsync<List<PitcherDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            pitchers.ForEach(x => rspDto.ShouldContain(y => y.Id == x.Id));
        }
        finally
        {
            // Clean up
            pitchersContext.RemoveRange(pitchers);
            playersContext.RemoveRange(players);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}