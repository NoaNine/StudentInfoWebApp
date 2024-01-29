using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.Core.Services.Interface
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCourses();
    }
}