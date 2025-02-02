using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BaseballStats.Tests;

public class DatabaseFixture : IDisposable
{
    public AppDbContext DbContext { get; init; } = new(new DbContextOptionsBuilder<AppDbContext>()
        .UseNpgsql("Host=localhost;Port=5432;Database=Baseball;Username=postgres;Password=wasd").Options);

    public void Dispose()
    {
        DbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}