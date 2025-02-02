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
            Id = Faker.Random.Long(1, 1000000)
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
            response.EnsureSuccessStatusCode();
            rspDto.ShouldNotBeNull();
            rspDto.Count.ShouldBe(seasons.Count);
            rspDto.OrderBy(x => x.Id).ToList().ShouldBe(seasons.Select(x => new SeasonDto() { Id = x.Id }).ToList());
        }
        finally
        {
            // Clean up
            seasonsContext.RemoveRange(seasons);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}