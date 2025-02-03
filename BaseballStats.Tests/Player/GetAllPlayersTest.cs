using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.Player;

public class GetAllPlayersTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllPlayers()
    {
        // Arrange
        var players = Enumerable.Range(1, 20).Select(_ => new Domain.Entities.Player()
        {
            Id = Faker.Random.Long(1, 1000000000)
        }).ToList();

        var playerContext = DatabaseFixture.DbContext.Set<Domain.Entities.Player>();
        await playerContext.AddRangeAsync(players);

        await DatabaseFixture.DbContext.SaveChangesAsync();
        
        try
        {
            // Act
            var response = await Admin.GetAsync("players/");
            var rspDto = await response.Content.ReadFromJsonAsync<List<RegularPlayerDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            players.ForEach(x => rspDto.ShouldContain(y => y.Id == x.Id));
        }
        finally
        {
            // Clean up
            playerContext.RemoveRange(players);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}