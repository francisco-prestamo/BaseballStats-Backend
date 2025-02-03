using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities.Identity;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Tests.Users;

public class GetAllUsersTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task GetAllUsersSuccess()
    {
        // Arrange
        var users = Enumerable.Range(1, 20).Select(_ => new RegisteredUser()
        {
            Id = Faker.Random.Long(1, 1000000000),
            Username = Faker.Name.FullName(),
            Password = Faker.Internet.Password(),
            Type = (UserTypes)Faker.Random.Int(0, Enum.GetValues<UserTypes>().Length - 1)
        }).ToList();

        var userContext = DatabaseFixture.DbContext.Set<RegisteredUser>();

        await userContext.AddRangeAsync(users);
        await DatabaseFixture.DbContext.SaveChangesAsync();

        try
        {
            // Act
            var response = await Admin.GetAsync("users");
            var rspDto = await response.Content.ReadFromJsonAsync<List<RegisteredUserDto>>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            rspDto.ShouldNotBeNull();
            users.ForEach(x => rspDto.ShouldContain(y => y.Id == x.Id));
        }
        finally
        {
            // Clean up
            userContext.RemoveRange(users);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}