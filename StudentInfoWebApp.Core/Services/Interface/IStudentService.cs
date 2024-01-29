using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.Core.Services.Interface
{
    public interface IStudentService
    {
        void DeleteStudent(Student student);
        void EditStudent(Student student);
    }
}