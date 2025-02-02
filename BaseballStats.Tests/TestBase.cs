using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BaseballStats.Tests;

public class TestBase : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseFixture>
{
    protected TestBase(WebApplicationFactory<Program> factory)
    {
        Client = factory.CreateClient();
    }

    protected TestBase(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture)
    {
        Client = factory.CreateClient();
        DatabaseFixture = databaseFixture;
    }


    protected HttpClient Client { get; init; }
    protected Faker Faker { get; init; } = new();
    protected DatabaseFixture DatabaseFixture { get; set; } = null!;
}