using StudentInfoWebApp.Core.Exceptions;
using StudentInfoWebApp.Core.Services.Base;
using StudentInfoWebApp.Core.Services.Interface;
using StudentInfoWebApp.DAL.UnitOfWork;
using StudentInfoWebApp.DAL.Models;

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
            throw new GroupNotNullOrEmptyException();
        }
        _unitOfWork.GetRepository<Group>().Delete(group);
        _unitOfWork.Save();
    }

    public async Task<IEnumerable<Group>> GetAllGroups()
    {
        var groups = await _unitOfWork.GetRepository<Group>().GetAll();
        await LoadStudentsToGroup(groups);
        return groups;
    }

    private async Task LoadStudentsToGroup(IEnumerable<Group> groups)
    {
        foreach (var group in groups)
        {
            var students = await _unitOfWork.GetRepository<Student>().GetAll(c => c.GroupId == group.Id);
            group.Students = (ICollection<Student>)students;
            group.Course = await _unitOfWork.GetRepository<Course>().GetById(group.CourseId);
        }
    }
}
