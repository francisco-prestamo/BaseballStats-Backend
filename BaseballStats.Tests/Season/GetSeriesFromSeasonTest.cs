using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.Season;

public class GetSeriesFromSeasonTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetSeriesFromSeason()
    {
        // Arrange
        var season = new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(1, 10000 - 1)
        };

        var series = Enumerable.Range(1, 20).Select(_ => new Domain.Entities.Series()
        {
            Id = Faker.Random.Long(8000000, 9000000),
            Name = Faker.Name.FirstName(),
            SeasonId = season.Id,
            Type = Faker.Name.LastName(),
            StartDate = Faker.Date.PastDateOnly(),
            EndDate = Faker.Date.PastDateOnly()
        }).OrderBy(x => x.Id).ToList();

        var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        var seriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();

        season = (await seasonContext.AddAsync(season)).Entity;
        await DatabaseFixture.DbContext.SaveChangesAsync();

        await seriesContext.AddRangeAsync(series);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync($"series/{season.Id}");
            var rspDto = await response.Content.ReadFromJsonAsync<List<SeriesDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            series.ForEach(x => rspDto.ShouldContain(new SeriesDto()
            {
                Id = x.Id,
                Name = x.Name,
                IdSeason = x.SeasonId,
                Type = x.Type,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            }));
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