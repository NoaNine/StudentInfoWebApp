using StudentInfoWebApp.DAL.Models.Base;
using System.Linq.Expressions;

namespace StudentInfoWebApp.DAL.Repository;

public interface IRepository<Entity> where Entity : BaseModel
{
    Task Delete(Entity entity);
    Task<IEnumerable<Entity>> GetAll(Expression<Func<Entity, bool>> filter = null);
    Task<Entity> GetById(object id);
    Task Insert(Entity entity);
    Task Update(Entity entity);
}