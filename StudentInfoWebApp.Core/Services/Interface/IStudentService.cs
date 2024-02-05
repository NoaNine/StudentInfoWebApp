using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.Core.Services.Interface
{
    public interface IStudentService
    {
        Task<Student> GetById(int id);
        void DeleteStudent(Student student);
        void EditStudent(Student student);
        Task<IEnumerable<Student>> GetAllStudents();
    }
}