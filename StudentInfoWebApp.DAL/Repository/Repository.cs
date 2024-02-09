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

    public async Task<Entity> GetById(object id) =>
        await _dbSet.FindAsync(id);

    public async Task<IEnumerable<Entity>> GetAll(Expression<Func<Entity, bool>>? filter = null)
    {
        if (filter is not null)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }
        return await _dbSet.ToListAsync();
    }

    public Task Insert(Entity entity)
    {
        _dbSet.AddAsync(entity);
        return Task.CompletedTask;
    }

    public Task Update(Entity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task Delete(Entity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

}
