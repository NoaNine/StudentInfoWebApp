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

    public void EditStudent(Student student)
    {
        _unitOfWork.GetRepository<Student>().Update(student);
        _unitOfWork.Save();
    }

    public void DeleteStudent(Student student)
    {
        _unitOfWork.GetRepository<Student>().Delete(student);
        _unitOfWork.Save();
    }
}
