using StudentInfoWebApp.DAL.Models.Base;
using System.Linq.Expressions;

namespace StudentInfoWebApp.DAL.Repository;

public interface IRepository<Entity> where Entity : BaseModel
{
    void Delete(Entity entity);
    ValueTask<IEnumerable<Entity>> GetAllAsync(Expression<Func<Entity, bool>> filter = null);
    ValueTask<Entity> GetByIdAsync(int id);
    void Insert(Entity entity);
    void Update(Entity entity);
}