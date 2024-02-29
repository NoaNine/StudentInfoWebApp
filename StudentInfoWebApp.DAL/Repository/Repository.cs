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

    public async ValueTask<Entity> GetByIdAsync(int id) =>
        await _dbSet.FirstOrDefaultAsync(i => i.Id == id);

    public async ValueTask<IEnumerable<Entity>> GetAllAsync(Expression<Func<Entity, bool>>? filter = null)
    {
        if (filter is not null)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }
        return await _dbSet.ToListAsync();
    }

    public void Insert(Entity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(Entity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(Entity entity)
    {
        _dbSet.Remove(entity);
    }

}
