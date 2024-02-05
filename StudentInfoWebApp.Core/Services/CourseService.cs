using StudentInfoWebApp.Core.Exceptions;
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

    public async Task<Course> GetById(int id) =>
        await _unitOfWork.GetRepository<Course>().GetById(id);

    public void EditCourse(Course course)
    {
        _unitOfWork.GetRepository<Course>().Update(course);
        _unitOfWork.Save();
    }

    public void DeleteCourse(Course course)
    {
        if (course.Groups.Count > 0)
        {
            throw new CourseNotNullOrEmptyException();
        }
        _unitOfWork.GetRepository<Course>().Delete(course);
        _unitOfWork.Save();
    }

    public async Task<IEnumerable<Course>> GetAllCourses()
    {
        var courses = await _unitOfWork.GetRepository<Course>().GetAll();
        await LoadGroupsToCourse(courses);
        return courses;
    }

    private async Task LoadGroupsToCourse(IEnumerable<Course> courses)
    {
        foreach (var course in courses)
        {
            var groups = await _unitOfWork.GetRepository<Group>().GetAll(c => c.CourseId == course.Id);
            course.Groups = (ICollection<Group>)groups;
            await LoadStudentsToGroup(course.Groups);
        }
    }

    private async Task LoadStudentsToGroup(ICollection<Group> groups)
    {
        foreach (var group in groups)
        {
            var students = await _unitOfWork.GetRepository<Student>().GetAll();
            group.Students = (ICollection<Student>)students;
        }
    }
}
