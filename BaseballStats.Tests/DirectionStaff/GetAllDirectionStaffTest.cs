using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Tests.GetAllDirectionStaff;

public class GetAllDirectionStaffTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllDirectionStaff()
    {
        // Arrange
        var directionStaff = Enumerable.Range(1, 5).Select(_ => new DirectionStaff()
        {
            Id = Faker.Random.Long(1, 1000000000),
            Name = Faker.Name.FullName(),
        }).ToList();

        var directionStaffContext = DatabaseFixture.DbContext.Set<DirectionStaff>();

        await directionStaffContext.AddRangeAsync(directionStaff);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("DirectionMembers");
            var rspDto = await response.Content.ReadFromJsonAsync<List<DirectionStaffDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            directionStaff.ForEach(x => rspDto.ShouldContain(y => y.Id == x.Id && y.Name == x.Name));
        }
        finally
        {
            // Clean up

            directionStaffContext.RemoveRange(directionStaff);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}