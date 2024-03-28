using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.Core.Services.Interface;

public interface ICourseService
{
    ValueTask<Course> GetByIdAsync(int id);
    void DeleteCourse(Course course);
    void EditCourse(Course course);
    Task<IEnumerable<Course>> GetAllCoursesAsync();
}