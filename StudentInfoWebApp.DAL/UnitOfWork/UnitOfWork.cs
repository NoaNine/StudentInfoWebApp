using Microsoft.EntityFrameworkCore;
using StudentInfoWebApp.DAL.Exceptions;
using StudentInfoWebApp.DAL.Models.Base;
using StudentInfoWebApp.DAL.Repository;

namespace StudentInfoWebApp.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly UniversityContext _context;
    private bool _disposed = false;

    public UnitOfWork(UniversityContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseModel
    {
        return new Repository<TEntity>(_context);
    }

    public async void SaveAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new DataAccessException(dbEx.Message, dbEx);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }

}
