using System.Net.Http.Headers;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Auth;
using BaseballStats.Domain.Entities.Identity;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Tests;

public class TestBase : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseFixture>
{
    protected TestBase(WebApplicationFactory<Program> factory)
    {
        Client = factory.CreateClient();
    }

    protected TestBase(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture) : this(factory)
    {
        DatabaseFixture = databaseFixture;
        
        var adminToken = GetAdminAccess();
        Admin = factory.CreateClient();
        Admin.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
    }

    protected HttpClient Client { get; init; }
    protected HttpClient Admin { get; init; }
    protected Faker Faker { get; init; } = new();
    protected DatabaseFixture DatabaseFixture { get; set; } = null!;

    private string GetAdminAccess()
    {
        var admin = new RegisteredUser()
        {
            Username = Faker.Internet.UserName(),
            Password = Faker.Internet.Password(),
            Type = UserTypes.Admin
        };

        var usersContext = DatabaseFixture.DbContext.Set<RegisteredUser>();
        admin = usersContext.Add(admin).Entity;
        DatabaseFixture.DbContext.SaveChanges();

        var request = new LoginCommand()
        {
            Username = admin.Username,
            Password = admin.Password
        };

        var response = Client.PostAsync("auth/login", JsonContent.Create(request)).Result;
        var rspDto = response.Content.ReadFromJsonAsync<RegisteredUserDto>().Result;

        usersContext.Remove(admin);
        DatabaseFixture.DbContext.SaveChanges();

        return rspDto is null ? "" : rspDto.Token![7..];
    }
}