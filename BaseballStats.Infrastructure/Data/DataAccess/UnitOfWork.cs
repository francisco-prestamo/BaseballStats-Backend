using BaseballStats.Domain.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DataAccess;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();

    public List<T> FromRawSql<T>(string query)
    {
        return context.Database.SqlQueryRaw<T>(query).ToList<T>();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);

        if (_repositories.TryGetValue(type, out var repository))
            return (IGenericRepository<TEntity>)repository;

        var implementationType = typeof(GenericRepository<>).MakeGenericType(typeof(TEntity));

        var instance = Activator.CreateInstance(implementationType, context);

        if (instance != null)
            _repositories.Add(type, instance);

        return (IGenericRepository<TEntity>)_repositories[type];
    }

    public int SaveChanges()
    {
        return context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this); /* ??? */
    }
}