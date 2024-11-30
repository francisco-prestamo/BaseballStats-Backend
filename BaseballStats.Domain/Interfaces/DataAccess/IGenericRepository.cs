using System.Linq.Expressions;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Domain.Interfaces.DataAccess;

/// <summary>
/// Generic repository interface for data access operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Filters entities based on a predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <returns>An IEnumerable of filtered entities.</returns>
    IEnumerable<TEntity> Where(Func<TEntity, bool> predicate);
    
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets an entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
    Task<TEntity?> GetByIdAsync(long id);

    /// <summary>
    /// Gets all entities asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of all entities.</returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deleted entity if found; otherwise, null.</returns>
    Task<TEntity?> DeleteAsync(long id);


    /// <summary>
    /// Executes a raw SQL query to get entities asynchronously.
    /// </summary>
    /// <param name="sql">The raw SQL query.</param>
    /// <param name="parameters">The parameters to pass to the SQL query.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities.</returns>
    /// <remarks>
    /// The SQL query should return all columns for the entity type.
    /// </remarks>
    Task<List<TEntity>> FromRawSqlAsync(string sql, params object[] parameters);
}