using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.Series;

public class GetSerieTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetSerie()
    {
        // Arrange
        var season = new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(10000, 1000000)
        };

        var serie = new Domain.Entities.Series()
        {
            Id = Faker.Random.Long(10000, 1000000),
            SeasonId = season.Id,
            Name = Faker.Lorem.Word(),
            Type = Faker.Lorem.Word(),
            StartDate = Faker.Date.PastDateOnly(),
            EndDate = Faker.Date.PastDateOnly()
        };

        var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        var seriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();

        await seasonContext.AddAsync(season);
        await seriesContext.AddAsync(serie);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync($"series/{season.Id}/{serie.Id}");
            var rspDto = await response.Content.ReadFromJsonAsync<SeriesDto>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            rspDto.ShouldBe(new SeriesDto()
            {
                Id = serie.Id,
                IdSeason = season.Id,
                Name = serie.Name,
                Type = serie.Type,
                StartDate = serie.StartDate,
                EndDate = serie.EndDate
            });
        }
        finally
        {
            // Clean up
            seriesContext.Remove(serie);
            seasonContext.Remove(season);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task GetSerieNotFound()
    {
        // Arrange

        try
        {
            // Act
            var response = await Admin.GetAsync($"series/{Faker.Random.Long(10000, 1000000)}/{Faker.Random.Long(10000, 1000000)}");
            var errorRsp = await response.Content.ReadFromJsonAsync<ErrorResponse>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            errorRsp.ShouldNotBeNull();
            errorRsp.Errors["generalErrors"].ShouldContain("Serie not found");
        }
        finally
        {
            // Clean up
        }
    }
    
    [Fact]
    public async Task GetSerieNotFoundInSeason()
    {
        // Arrange
        var season = new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(10000, 1000000)
        };

        var serie = new Domain.Entities.Series()
        {
            Id = Faker.Random.Long(10000, 1000000),
            SeasonId = season.Id,
            Name = Faker.Lorem.Word(),
            Type = Faker.Lorem.Word(),
            StartDate = Faker.Date.PastDateOnly(),
            EndDate = Faker.Date.PastDateOnly()
        };

        var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        var seriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();

        await seasonContext.AddAsync(season);
        await seriesContext.AddAsync(serie);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync($"series/{Faker.Random.Long(1000000 + 1, 2000000)}/{serie.Id}");
            var errorRsp = await response.Content.ReadFromJsonAsync<ErrorResponse>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            errorRsp.ShouldNotBeNull();
            errorRsp.Errors["generalErrors"].ShouldContain("Serie not found in the season");
        }
        finally
        {
            // Clean up
            seriesContext.Remove(serie);
            seasonContext.Remove(season);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}