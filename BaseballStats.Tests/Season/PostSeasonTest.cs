using System.Net;
using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.Season;

public class PostSeasonTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task PostSeasonSuccess()
    {
        // Arrange
        var season = new SeasonDto()
        {
            Id = Faker.Random.Long(2001000, 2002000)
        };

        try
        {
            // Act
            var response = await Admin.PostAsync("seasons/", JsonContent.Create(season));
            var rspDto = await response.Content.ReadFromJsonAsync<SeasonDto>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            rspDto.ShouldNotBeNull();
            rspDto.ShouldBe(season);
        }
        finally
        {
            // Clean up
            var seasonsContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
            var seasonEntity = await seasonsContext.FindAsync(season.Id);
            if (seasonEntity != null)
            {
                seasonsContext.Remove(seasonEntity);
                await DatabaseFixture.DbContext.SaveChangesAsync();
            }
        }
    }

    [Fact]
    public async Task PostSeasonConflict()
    {
        // Arrange
        var season = new SeasonDto()
        {
            Id = Faker.Random.Long(2002001, 3003000)
        };

        var seasonsContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        await seasonsContext.AddAsync(new Domain.Entities.Season()
        {
            Id = season.Id
        });
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.PostAsync("seasons/", JsonContent.Create(season));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        }
        finally
        {
            // Clean up
            var seasonEntity = await seasonsContext.FindAsync(season.Id);
            if (seasonEntity != null)
            {
                seasonsContext.Remove(seasonEntity);
                await DatabaseFixture.DbContext.SaveChangesAsync();
            }
        }
    }

    [Fact]
    public async Task PostSeasonBadRequest_WrongId()
    {
        // Arrange
        var season = new SeasonDto()
        {
            Id = Faker.Random.Long(-10000, 0)
        };

        try
        {
            // Act
            var response = await Admin.PostAsync("seasons/", JsonContent.Create(season));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        finally
        {
            // Clean up
            var seasonsContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
            var seasonEntity = await seasonsContext.FindAsync(season.Id);
            if (seasonEntity != null)
            {
                seasonsContext.Remove(seasonEntity);
                await DatabaseFixture.DbContext.SaveChangesAsync();
            }
        }
    }

    [Fact]
    public async Task PostSeasonBadRequest_NullId()
    {
        // Arrange

        try
        {
            // Act
            var response = await Admin.PostAsync("seasons/", JsonContent.Create(new { }));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
        finally
        {
            // Clean up
        }
    }
}