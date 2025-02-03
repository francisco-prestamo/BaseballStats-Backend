using System.Net;
using BaseballStats.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BaseballStats.Tests.Season;

public class DeleteSeasonTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task DeleteSeasonSuccess()
    {
        // Arrange
        var season = new SeasonDto()
        {
            Id = Faker.Random.Long(3001000, 3002000)
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
            var response = await Admin.DeleteAsync($"seasons/{season.Id}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var seasonEntity = await seasonsContext.FirstOrDefaultAsync(x => x.Id == season.Id);
            seasonEntity.ShouldBeNull();
        }
        finally
        {
            // Clean up
            var seasonEntity = await seasonsContext.FirstOrDefaultAsync(x => x.Id == season.Id);
            if (seasonEntity != null)
            {
                seasonsContext.Remove(seasonEntity);
                await DatabaseFixture.DbContext.SaveChangesAsync();
            }
        }
    }

    [Fact]
    public async Task DeleteSeasonNotFound()
    {
        // Arrange
        var season = new SeasonDto()
        {
            Id = Faker.Random.Long(3002001, 3003000)
        };

        // Act
        var response = await Admin.DeleteAsync($"seasons/{season.Id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteSeasonInvalidId()
    {
        // Arrange
        var season = new SeasonDto()
        {
            Id = Faker.Random.Long(-1000000, 0)
        };

        // Act
        var response = await Admin.DeleteAsync($"seasons/{season.Id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}