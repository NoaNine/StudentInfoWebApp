using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.Core.Services.Interface;

public interface IGroupService
{
    void DeleteGroup(Group group);
    void EditGroup(Group group);
    Task<IEnumerable<Group>> GetAllGroups();
}