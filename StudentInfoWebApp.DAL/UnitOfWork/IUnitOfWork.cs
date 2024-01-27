using StudentInfoWebApp.DAL.Models.Base;
using StudentInfoWebApp.DAL.Repository;

namespace StudentInfoWebApp.DAL.UnitOfWork;

public interface IUnitOfWork
{
    void Dispose();
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseModel;
    void Save();
}