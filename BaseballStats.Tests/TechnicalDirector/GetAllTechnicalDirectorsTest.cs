using BaseballStats.Application.DTOs;

namespace BaseballStats.Tests.TechnicalDirector;

public class GetAllTechnicalDirectorsTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllTechnicalDirectors()
    {
        // Arrange
        var technicalDirectors = Enumerable.Range(1, 5).Select(_ => new Domain.Entities.Identity.TechnicalDirector()
        {
            Id = Faker.Random.Long(1, 1000000000)
        }).ToList();

        var technicalDirectorContext = DatabaseFixture.DbContext.Set<Domain.Entities.Identity.TechnicalDirector>();

        await technicalDirectorContext.AddRangeAsync(technicalDirectors);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("technicalDirectors");
            var rspDto = await response.Content.ReadFromJsonAsync<List<RegisteredUserDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            technicalDirectors.ForEach(x => rspDto.ShouldContain(y => y.Id == x.Id));
        }
        finally
        {
            // Clean up
            technicalDirectorContext.RemoveRange(technicalDirectors);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}