using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities.Identity;

namespace BaseballStats.Tests.Team;

public class GetTeamTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetTeamSuccess()
    {
        // Arrange
        var technicalDirector = new Domain.Entities.Identity.TechnicalDirector()
        {
            Id = Faker.Random.Long(1, 1000000000)
        };

        var team = new Domain.Entities.Team()
        {
            Id = Faker.Random.Long(1, 1000000000),
            Name = Faker.Lorem.Word(),
            Color = Faker.Lorem.Word(),
            Initials = "ASD",
            TechnicalDirectorId = technicalDirector.Id,
            RepresentedEntity = Faker.Lorem.Word()
        };

        var technicalDirectorContext = DatabaseFixture.DbContext.Set<Domain.Entities.Identity.TechnicalDirector>();
        var teamContext = DatabaseFixture.DbContext.Set<Domain.Entities.Team>();

        await technicalDirectorContext.AddAsync(technicalDirector);
        await teamContext.AddAsync(team);

        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync($"teams/{team.Id}");
            var rspDto = await response.Content.ReadFromJsonAsync<TeamDto>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            rspDto.ShouldBe(new TeamDto()
            {
                Id = team.Id,
                Name = team.Name,
                Color = team.Color,
                Initials = team.Initials,
                DtId = technicalDirector.Id,
                RepresentedEntity = team.RepresentedEntity
            });
        }
        finally
        {
            // Clean up
            teamContext.Remove(team);
            technicalDirectorContext.Remove(technicalDirector);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task GetTeamNotFound()
    {
        // Arrange

        try
        {
            // Act
            var response = await Admin.GetAsync($"teams/{Faker.Random.Long(1, 1000000000)}");
            var rspDto = await response.Content.ReadFromJsonAsync<ErrorResponse>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            rspDto.ShouldNotBeNull();
            rspDto.Errors["generalErrors"].ShouldContain("TeamId not found");
        }
        finally
        {
            // Clean up
        }
    }
}