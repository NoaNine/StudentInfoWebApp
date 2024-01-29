using StudentInfoWebApp.Core.Exceptions;
using StudentInfoWebApp.Core.Services.Base;
using StudentInfoWebApp.Core.Services.Interface;
using StudentInfoWebApp.DAL.Models;
using StudentInfoWebApp.DAL.UnitOfWork;

namespace StudentInfoWebApp.Core.Services;

public class GroupService : BaseService, IGroupService
{
    public GroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {

    }

    public void EditGroup(Group group)
    {
        _unitOfWork.GetRepository<Group>().Update(group);
        _unitOfWork.Save();
    }

    public void DeleteGroup(Group group)
    {
        if (group.Students.Count > 0)
        {
            throw new GroupNotNullOrEmpty();
        }
        _unitOfWork.GetRepository<Group>().Delete(group);
        _unitOfWork.Save();
    }
}
