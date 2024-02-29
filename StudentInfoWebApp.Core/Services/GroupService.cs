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

    public async ValueTask<Group> GetByIdAsync(int id) =>
        await _unitOfWork.GetRepository<Group>().GetByIdAsync(id).ConfigureAwait(false);

    public void EditGroup(Group group)
    {
        _unitOfWork.GetRepository<Group>().Update(group);
        _unitOfWork.SaveAsync();
    }

    public async ValueTask DeleteGroupAsync(Group group)
    {
        var students = await _unitOfWork.GetRepository<Student>().GetAllAsync(_ => _.GroupId == group.Id).ConfigureAwait(false);
        group.Students = (ICollection<Student>)students;
        if (group.Students.Count > 0)
        {
            throw new GroupNotNullOrEmptyException();
        }
        _unitOfWork.GetRepository<Group>().Delete(group);
        _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<Group>> GetAllGroupsAsync()
    {
        var groups = await _unitOfWork.GetRepository<Group>().GetAllAsync().ConfigureAwait(false);
        await LoadStudentsToGroup(groups);
        return groups;
    }

    private async Task LoadStudentsToGroup(IEnumerable<Group> groups)
    {
        foreach (var group in groups)
        {
            var students = await _unitOfWork.GetRepository<Student>().GetAllAsync(c => c.GroupId == group.Id).ConfigureAwait(false);
            group.Students = (ICollection<Student>)students;
            group.Course = await _unitOfWork.GetRepository<Course>().GetByIdAsync(group.CourseId).ConfigureAwait(false);
        }
    }
}
