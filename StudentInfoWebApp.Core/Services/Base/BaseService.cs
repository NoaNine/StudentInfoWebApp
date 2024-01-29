using StudentInfoWebApp.DAL.UnitOfWork;

namespace StudentInfoWebApp.Core.Services.Base;

public abstract class BaseService
{
    private protected readonly IUnitOfWork _unitOfWork;

    public BaseService(IUnitOfWork unitOfWork) 
    { 
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
}
