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

    public async ValueTask<Course> GetByIdAsync(int id) =>
        await _unitOfWork.GetRepository<Course>().GetByIdAsync(id).ConfigureAwait(false);

    public void EditCourse(Course course)
    {
        _unitOfWork.GetRepository<Course>().Update(course);
        _unitOfWork.SaveAsync();
    }

    public void DeleteCourse(Course course)
    {
        if (course.Groups.Count > 0)
        {
            throw new CourseNotNullOrEmptyException();
        }
        _unitOfWork.GetRepository<Course>().Delete(course);
        _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        var courses = await _unitOfWork.GetRepository<Course>().GetAllAsync().AsTask();
        await LoadGroupsToCourse(courses);
        return courses;
    }

    private async Task LoadGroupsToCourse(IEnumerable<Course> courses)
    {
        foreach (var course in courses)
        {
            var groups = await _unitOfWork.GetRepository<Group>().GetAllAsync(c => c.CourseId == course.Id).ConfigureAwait(false);
            course.Groups = (ICollection<Group>)groups;
            await LoadStudentsToGroup(course.Groups);
        }
    }

    private async Task LoadStudentsToGroup(ICollection<Group> groups)
    {
        foreach (var group in groups)
        {
            var students = await _unitOfWork.GetRepository<Student>().GetAllAsync().ConfigureAwait(false);
            group.Students = (ICollection<Student>)students;
        }
    }
}
