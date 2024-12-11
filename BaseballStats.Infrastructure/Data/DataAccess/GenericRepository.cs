using System.Linq.Expressions;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DataAccess;

public class GenericRepository<TEntity>(AppDbContext context) : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly AppDbContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<TEntity?> GetByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public async Task<TEntity?> DeleteAsync(long id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }

        return entity;
    }
}