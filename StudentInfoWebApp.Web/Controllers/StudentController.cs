using Microsoft.AspNetCore.Mvc;
using StudentInfoWebApp.Core.Services.Interface;
using StudentInfoWebApp.DAL.Models;
using StudentInfoWebApp.Web.Models;
using System.Diagnostics;

namespace StudentInfoWebApp.Web.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
    }

    public async Task<IActionResult> IndexAsync(int groupId)
    {
        var students = await _studentService.GetAllStudentsAsync();
        if (groupId != 0)
        {
            students = students.Where(_ => _.GroupId == groupId);
        }
        return View(students);
    }

    [HttpPost]
    public async Task<IActionResult> IndexAsync(string searchString)
    {
        var students = await _studentService.GetAllStudentsAsync();
        if (!string.IsNullOrEmpty(searchString))
        {
            students = students.Where(_ => _.LastName.Contains(searchString));
        }
        return View(students);
    }

    public async Task<IActionResult> EditAsync(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        IsNull(student);
        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(int id, [Bind("Id,FirstName,LastName,Group,GroupId")] Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        _studentService.EditStudent(student);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        IsNull(student);
        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAsync(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        IsNull(student);
        _studentService.DeleteStudent(student);

        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private IActionResult IsNull(Student student)
    {
        if (student == null)
        {
            return NotFound();
        }
        return Ok();
    }
}
