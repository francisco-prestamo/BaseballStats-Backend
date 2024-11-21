using BaseballStats.Domain.Interfaces.DataAccess;

namespace Infrastructure.Data.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        Type type = typeof(TEntity);

        if (!_repositories.ContainsKey(type))
        {
            Type implementationType = typeof(GenericRepository<>).MakeGenericType(typeof(TEntity));

            object? instance = Activator.CreateInstance(implementationType, _context);

            if (instance != null)
            {
                _repositories.Add(type, instance);
            }
        }

        return (IGenericRepository<TEntity>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this); /* ??? */
    }
}