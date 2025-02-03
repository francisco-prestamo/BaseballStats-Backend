using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities.Identity;

namespace BaseballStats.Tests.Team;

public class GetAllTeamsTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllTeamsSuccess()
    {
        // Arrange
        var technicalDirector = new TechnicalDirector()
        {
            Id = Faker.Random.Long(1, 1000000000)
        };

        var teams = Enumerable.Range(1, 20).Select(_ => new Domain.Entities.Team()
        {
            Id = Faker.Random.Long(1, 1000000000),
            Name = Faker.Lorem.Word(),
            Color = Faker.Lorem.Word(),
            Initials = "ASD",
            TechnicalDirectorId = technicalDirector.Id,
            RepresentedEntity = Faker.Lorem.Word()
        }).ToList();

        var technicalDirectorContext = DatabaseFixture.DbContext.Set<TechnicalDirector>();
        var teamContext = DatabaseFixture.DbContext.Set<Domain.Entities.Team>();

        await technicalDirectorContext.AddAsync(technicalDirector);
        await teamContext.AddRangeAsync(teams);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("teams/");
            var rspDto = await response.Content.ReadFromJsonAsync<List<TeamDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            teams.ForEach(x => rspDto.ShouldContain(y => y.Id == x.Id));
        }
        finally
        {
            // Clean up
            teamContext.RemoveRange(teams);
            technicalDirectorContext.Remove(technicalDirector);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}