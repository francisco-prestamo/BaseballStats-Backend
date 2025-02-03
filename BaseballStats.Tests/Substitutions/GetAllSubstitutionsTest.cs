namespace BaseballStats.Tests.Substitutions;

public class GetAllSubstitutionsTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllSubstitutions()
    {
    }
}