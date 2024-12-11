namespace BaseballStats.Domain.Interfaces.DataAccess;

/// <summary>
/// Represents a unit of work that encapsulates a set of operations to be performed as a single transaction.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>The repository for the specified entity type.</returns>
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

    /// <summary>
    /// Saves all changes made in this unit of work.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    int SaveChanges();

    /// <summary>
    /// Asynchronously saves all changes made in this unit of work.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified raw SQL query.
    /// </summary>
    /// <typeparam name="T">The type of the elements returned by the query.</typeparam>
    /// <param name="query">The raw SQL query.</param>
    /// <returns>The elements returned by the query.</returns>
    /// <remarks>
    /// The query must return instances of the type specified by the type parameter <typeparamref name="T"/>.
    /// </remarks>
    List<T> FromRawSql<T>(string query);
}