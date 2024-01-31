using StudentInfoWebApp.Core.Services.Base;
using StudentInfoWebApp.Core.Services.Interface;
using StudentInfoWebApp.DAL.Models;
using StudentInfoWebApp.DAL.UnitOfWork;

namespace StudentInfoWebApp.Core.Services;

public class CourseService : BaseService, ICourseService
{
    public CourseService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {

    }

    public async Task<IEnumerable<Course>> GetAllCourses(bool addNestedData = false)
    {
        var courses = await _unitOfWork.GetRepository<Course>().GetAll();
        if (addNestedData is true)
        {
            LoadGroups(courses);
        }
        return courses;
    }

    private Task LoadGroups(IEnumerable<Course> courses)
    {
        foreach (var course in courses)
        {
            course.Groups = (ICollection<Group>)_unitOfWork.GetRepository<Group>().GetAll(c => c.CourseId == course.Id);
            LoadStudents(course.Groups);
        }
        return Task.CompletedTask;
    }

    private Task LoadStudents(ICollection<Group> groups)
    {
        foreach (var group in groups)
        {
            group.Students = (ICollection<Student>)_unitOfWork.GetRepository<Student>().GetAll();
        }
        return Task.CompletedTask;
    }
}
