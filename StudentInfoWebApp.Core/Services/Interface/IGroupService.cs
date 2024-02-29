using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.Core.Services.Interface;

public interface IGroupService
{
    ValueTask<Group> GetByIdAsync(int id);
    ValueTask DeleteGroupAsync(Group group);
    void EditGroup(Group group);
    Task<IEnumerable<Group>> GetAllGroupsAsync();
}