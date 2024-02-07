using StudentInfoWebApp.DAL.Models;

namespace StudentInfoWebApp.Core.Services.Interface;

public interface ICourseService
{
    Task<Course> GetById(int id);
    void DeleteCourse(Course course);
    void EditCourse(Course course);
    Task<IEnumerable<Course>> GetAllCourses();
}