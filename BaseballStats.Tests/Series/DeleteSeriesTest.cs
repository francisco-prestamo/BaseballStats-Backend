using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Series.Delete;
using Microsoft.EntityFrameworkCore;

namespace BaseballStats.Tests.Series;

public class DeleteSeriesTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task DeleteSeriesSuccess()
    {
        // // Arrange
        // var command = new DeleteSeriesCommand()
        // {
        //     Id = Faker.Random.Long(3001000, 3002000),
        //     SeasonId = Faker.Random.Long(5001000, 5002000)
        // };
        //
        // var seasonContext = DatabaseFixture.DbContext.Set<Domain.Entities.Season>();
        // var seriesContext = DatabaseFixture.DbContext.Set<Domain.Entities.Series>();
        //
        // await seasonContext.AddAsync(new Domain.Entities.Season()
        // {
        //     Id = command.SeasonId
        // });
        // await seriesContext.AddAsync(new Domain.Entities.Series()
        // {
        //     Id = command.Id,
        //     SeasonId = command.SeasonId
        // });
        // await DatabaseFixture.DbContext.SaveChangesAsync();
        //
        // try
        // {
        //     // Act
        //     var response = await Admin.DeleteAsync($"series/{command.SeasonId}/{command.Id}");
        //
        //     // Assert
        //     response.StatusCode.ShouldBe(HttpStatusCode.OK);
        //     var seriesEntity = await seriesContext.FirstOrDefaultAsync(x => x.Id == command.Id);
        //     seriesEntity.ShouldBeNull();
        // }
        // finally
        // {
        //     // Clean up
        //     var seriesEntity = await seriesContext.FirstOrDefaultAsync(x => x.Id == command.Id);
        //     if (seriesEntity != null)
        //     {
        //         seriesContext.Remove(seriesEntity);
        //         await DatabaseFixture.DbContext.SaveChangesAsync();
        //     }
        //
        //     var seasonEntity = await seasonContext.FirstOrDefaultAsync(x => x.Id == command.SeasonId);
        //     if (seasonEntity != null)
        //     {
        //         seasonContext.Remove(seasonEntity);
        //         await DatabaseFixture.DbContext.SaveChangesAsync();
        //     }
        // }
    }
    
    [Fact]
    public async Task DeleteSeriesNotFound()
    {
        // Arrange
        var command = new DeleteSeriesCommand()
        {
            Id = Faker.Random.Long(3002001, 3003000),
            SeasonId = Faker.Random.Long(5002001, 5003000)
        };
        
        // Act
        var response = await Admin.DeleteAsync($"series/{command.SeasonId}/{command.Id}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeleteSeriesInvalidId()
    {
        // Arrange
        var command = new DeleteSeriesCommand()
        {
            Id = Faker.Random.Long(-1000000, 0),
            SeasonId = Faker.Random.Long(-1000000, 0)
        };
        
        // Act
        var response = await Admin.DeleteAsync($"series/{command.SeasonId}/{command.Id}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}