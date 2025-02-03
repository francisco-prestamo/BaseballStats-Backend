using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.Season;

public class GetSeasonsTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetSeasons()
    {
        // Arrange
        var seasons = Enumerable.Range(1, 20).Select(_ => new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(10000, 1000000)
        }).OrderBy(x => x.Id).ToList();

        var seasonsContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        await seasonsContext.AddRangeAsync(seasons);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("seasons/");
            var rspDto = await response.Content.ReadFromJsonAsync<List<SeasonDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            seasons.ForEach(x => rspDto.ShouldContain(new SeasonDto() { Id = x.Id }));
        }
        finally
        {
            // Clean up
            seasonsContext.RemoveRange(seasons);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}