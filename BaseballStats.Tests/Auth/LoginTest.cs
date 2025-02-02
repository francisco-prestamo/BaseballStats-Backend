using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Auth;
using BaseballStats.Domain.Entities.Identity;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Tests.Auth;

public class LoginTest(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : TestBase(factory, databaseFixture)
{
    [Fact]
    public async Task LoginFailed()
    {
        // Arrange
        var user = new LoginCommand()
        {
            Username = Faker.Internet.UserName(),
            Password = Faker.Internet.Password()
        };

        // Act
        var response = await Client.PostAsync($"auth/login", JsonContent.Create(user));
        var rspObj = await response.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        response.IsSuccessStatusCode.ShouldBeFalse();
        rspObj.ShouldNotBeNull();
        rspObj.Errors["generalErrors"][0].ShouldBe("Invalid login credentials!");
    }

    [Fact]
    public async Task LoginSuccess()
    {
        // Arrange
        var user = new RegisteredUser()
        {
            Username = Faker.Internet.UserName(),
            Password = Faker.Internet.Password(),
            Type = (UserTypes)new Random().Next(Enum.GetValues<UserTypes>().Length)
        };

        var usersContext = DatabaseFixture.DbContext.Set<RegisteredUser>();
        user = (await usersContext.AddAsync(user)).Entity;
        await DatabaseFixture.DbContext.SaveChangesAsync();

        var request = new LoginCommand()
        {
            Username = user.Username,
            Password = user.Password
        };

        try
        {
            // Act
            var response = await Client.PostAsync($"auth/login", JsonContent.Create(request));
            var rspDto = await response.Content.ReadFromJsonAsync<RegisteredUserDto>();

            // Assert
            response.EnsureSuccessStatusCode();
            rspDto.ShouldNotBeNull();
            rspDto.Username.ShouldBe(user.Username);
            rspDto.Token.ShouldNotBeNullOrEmpty();
        }
        finally
        {
            // Clean up
            usersContext.Remove(user);
            await DatabaseFixture.DbContext.SaveChangesAsync();
        }
    }
}