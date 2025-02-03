using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Series.Post;

namespace BaseballStats.Tests.Series;

public class PostSeriesTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task PostSeriesSuccess()
    {
        // Arrange
        var season = new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(5000000, 6000000)
        };

        var series = new PostSeriesCommand()
        {
            Id = Faker.Random.Long(2001000, 2002000),
            Name = Faker.Lorem.Word(),
            IdSeason = season.Id,
            StartDate = Faker.Date.Past(),
            EndDate = Faker.Date.Past(),
            Type = Faker.Lorem.Word()
        };

        var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        var seriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();

        await seasonContext.AddAsync(season);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.PostAsync("series/", JsonContent.Create(series));
            var rspDto = await response.Content.ReadFromJsonAsync<SeriesDto>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            rspDto.ShouldNotBeNull();
            rspDto.Id.ShouldBe(series.Id);
            rspDto.Name.ShouldBe(series.Name);
            rspDto.IdSeason.ShouldBe(series.IdSeason);
            rspDto.Type.ShouldBe(series.Type);
        }
        finally
        {
            // Clean up
            var seriesEntity = await seriesContext.FindAsync(series.Id);
            if (seriesEntity != null)
            {
                seriesContext.Remove(seriesEntity);
                await DatabaseFixture.DbContext.SaveChangesAsync();
            }

            var seasonEntity = await seasonContext.FindAsync(season.Id);
            if (seasonEntity != null)
            {
                seasonContext.Remove(seasonEntity);
                await DatabaseFixture.DbContext.SaveChangesAsync();
            }
        }
    }

    [Fact]
    public async Task PostSeriesConflict()
    {
        // Arrange
        var season = new Domain.Entities.Season()
        {
            Id = Faker.Random.Long(6000001, 7000000)
        };

        var command = new PostSeriesCommand()
        {
            Id = Faker.Random.Long(2002001, 3003000),
            Name = Faker.Lorem.Word(),
            IdSeason = season.Id,
            StartDate = Faker.Date.Past(),
            EndDate = Faker.Date.Past(),
            Type = Faker.Lorem.Word()
        };

        var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        var seriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();

        await seasonContext.AddAsync(season);
        await seriesContext.AddAsync(new Domain.Entities.Series()
        {
            Id = command.Id,
            SeasonId = command.IdSeason,
            Name = command.Name,
            StartDate = new DateOnly(command.StartDate.Year, command.StartDate.Month, command.StartDate.Day),
            EndDate = new DateOnly(command.EndDate.Year, command.EndDate.Month, command.EndDate.Day),
            Type = command.Type
        });
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.PostAsync("series/", JsonContent.Create(command));
            var errorRsp = await response.Content.ReadFromJsonAsync<ErrorResponse>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
            errorRsp.ShouldNotBeNull();
            errorRsp.Errors["generalErrors"].ShouldContain("Series Id already exists.");
        }
        finally
        {
            // Clean up
            var seriesEntity = await seriesContext.FindAsync(command.Id);
            if (seriesEntity != null)
            {
                seriesContext.Remove(seriesEntity);
                await DatabaseFixture.DbContext.SaveChangesAsync();
            }

            var seasonEntity = await seasonContext.FindAsync(season.Id);
            if (seasonEntity != null)
            {
                seasonContext.Remove(seasonEntity);
                await DatabaseFixture.DbContext.SaveChangesAsync();
            }
        }
    }

    [Fact]
    public async Task PostSeriesBadRequestSeasonIdNotFound()
    {
        // Arrange
        var command = new PostSeriesCommand()
        {
            Id = Faker.Random.Long(2002001, 3003000),
            Name = Faker.Lorem.Word(),
            IdSeason = Faker.Random.Long(5002001, 5003000),
            StartDate = Faker.Date.Past(),
            EndDate = Faker.Date.Past(),
            Type = Faker.Lorem.Word()
        };

        try
        {
            // Act
            var response = await Admin.PostAsync("series/", JsonContent.Create(command));
            var errorRsp = await response.Content.ReadFromJsonAsync<ErrorResponse>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            errorRsp.ShouldNotBeNull();
            errorRsp.Errors["generalErrors"].ShouldContain("Season Id not found.");
        }
        finally
        {
            // Clean up
        }
    }
}