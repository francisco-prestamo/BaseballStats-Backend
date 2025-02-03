using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.Series;

public class GetSeriesTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetSeries()
    {
        // Arrange
        var season = new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(1, 1000000000)
        };
        
        var series = Enumerable.Range(1, 20).Select(_ => new Domain.Entities.Series()
        {
            Id = Faker.Random.Long(10000, 1000000),
            SeasonId = season.Id,
            Name = Faker.Lorem.Word(),
            Type = Faker.Lorem.Word(),
            StartDate = Faker.Date.PastDateOnly(),
            EndDate = Faker.Date.FutureDateOnly()
        }).ToList();

        var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        var seriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();
        
        await seasonContext.AddAsync(season);
        await seriesContext.AddRangeAsync(series);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("series/");
            var rspDto = await response.Content.ReadFromJsonAsync<List<SeriesDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            series.ForEach(x => rspDto.ShouldContain(y => y.Id == x.Id));
        }
        finally
        {
            // Clean up
            seriesContext.RemoveRange(series);
            seasonContext.Remove(season);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}