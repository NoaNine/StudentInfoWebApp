using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.Core.Services.Interface;

public interface IGroupService
{
    Task<Group> GetById(int id);
    void DeleteGroup(Group group);
    void EditGroup(Group group);
    Task<IEnumerable<Group>> GetAllGroups();
}