using StudentInfoWebApp.Core.Services.Base;
using StudentInfoWebApp.Core.Services.Interface;
using StudentInfoWebApp.DAL.Models;
using StudentInfoWebApp.DAL.UnitOfWork;

namespace StudentInfoWebApp.Core.Services;

public class StudentService : BaseService, IStudentService
{
    public StudentService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {

    }

    public async ValueTask<Student> GetByIdAsync(int id) =>
        await _unitOfWork.GetRepository<Student>().GetByIdAsync(id).ConfigureAwait(false);

    public void EditStudent(Student student)
    {
        _unitOfWork.GetRepository<Student>().Update(student);
        _unitOfWork.SaveAsync();
    }

    public void DeleteStudent(Student student)
    {
        _unitOfWork.GetRepository<Student>().Delete(student);
        _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        var students = await _unitOfWork.GetRepository<Student>().GetAllAsync().ConfigureAwait(false);
        foreach (var student in students)
        {
            student.Group = await _unitOfWork.GetRepository<Group>().GetByIdAsync(student.GroupId).ConfigureAwait(false);
        }
        return students;
    }
}
