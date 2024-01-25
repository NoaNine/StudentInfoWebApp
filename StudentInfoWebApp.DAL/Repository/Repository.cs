using Microsoft.EntityFrameworkCore;
using StudentInfoWebApp.DAL.Models.Base;
using System.Linq.Expressions;

namespace StudentInfoWebApp.DAL.Repository;

public class Repository<Entity> : IRepository<Entity> where Entity : BaseModel
{
    private readonly UniversityContext _context;
    private readonly DbSet<Entity> _dbSet;
    public Repository(UniversityContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Entity>();
    }

    public virtual async Task<Entity> GetById(object id) =>
        await _dbSet.FindAsync(id);

    public virtual async Task<IEnumerable<Entity>> GetAll(Expression<Func<Entity, bool>> filter = null)
    {
        if (filter is null)
        {
            return await _dbSet.ToListAsync();
        }
        return await _dbSet.Where(filter).ToListAsync();
    }

    public virtual Task Insert(Entity entity)
    {
        _dbSet.Add(entity);
        return Task.CompletedTask;
    }

    public virtual Task Update(Entity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task Delete(Entity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

}
