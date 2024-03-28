using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.Core.Services.Interface;

public interface IStudentService
{
    ValueTask<Student> GetByIdAsync(int id);
    void DeleteStudent(Student student);
    void EditStudent(Student student);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
}