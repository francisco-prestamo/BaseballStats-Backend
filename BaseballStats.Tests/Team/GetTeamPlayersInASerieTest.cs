using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Entities.Identity;

namespace BaseballStats.Tests.Team;

public class GetTeamPlayersInASerieTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetTeamPlayersInASerieSuccess()
    {
    }
}