using System.Linq.Expressions;
using BaseballStats.Domain.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DataAccess;

public class GenericRepository<TEntity>(AppDbContext context) : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly AppDbContext _context = context;

    public DbSet<TEntity> DbSet { get; } = context.Set<TEntity>();

    public IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
    {
        return DbSet.Where(predicate);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<TEntity?> GetByIdAsync(long id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public async Task<TEntity?> DeleteAsync(long id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity != null)
        {
            DbSet.Remove(entity);
        }

        return entity;
    }

    public IEnumerable<TEntity> DropWhere(Func<TEntity, bool> predicate)
    {
        var entities = DbSet.Where(predicate);
        DbSet.RemoveRange(entities);
        return entities;
    } 

    public void AddRange(IEnumerable<TEntity> entities)
    {
        DbSet.AddRange(entities);
    }
}