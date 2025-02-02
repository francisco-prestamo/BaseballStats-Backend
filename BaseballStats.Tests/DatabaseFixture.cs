using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BaseballStats.Tests;

public class DatabaseFixture : IDisposable
{
    public AppDbContext DbContext { get; init; }

    public DatabaseFixture()
    {
        DbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=Baseball;Username=postgres;Password=wasd").Options);
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}