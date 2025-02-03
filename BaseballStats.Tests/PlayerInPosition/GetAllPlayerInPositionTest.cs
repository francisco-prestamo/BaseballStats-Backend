using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Tests.PlayerInPosition;

public class GetAllPlayerInPositionTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllPlayerInPosition()
    {
        // Arrange
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

        var playerContext = DatabaseFixture.DbContext.Set<Domain.Entities.Player>();
        var playerInPositionContext = DatabaseFixture.DbContext.Set<Domain.Entities.PlayerInPosition>();

        await playerContext.AddRangeAsync(players);
        await playerInPositionContext.AddRangeAsync(playersInPosition);

        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("playerInPositions");
            var rspDto = await response.Content.ReadFromJsonAsync<List<PlayerInPositionCRUDDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            playersInPosition.ForEach(x => rspDto.ShouldContain(y => y.PlayerId == x.PlayerId));
        }
        finally
        {
            // Clean up
            playerInPositionContext.RemoveRange(playersInPosition);
            playerContext.RemoveRange(players);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}