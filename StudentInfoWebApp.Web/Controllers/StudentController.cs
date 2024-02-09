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

    public async Task<IActionResult> Index(int groupId)
    {
        try
        {
            var students = await _studentService.GetAllStudents();
            if (groupId != 0)
            {
                students = students.Where(_ => _.GroupId == groupId);
            }
            return View(students);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Index(string searchString)
    {
        try
        {
            var students = await _studentService.GetAllStudents();
            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(_ => _.LastName.Contains(searchString));
            }
            return View(students);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var student = await _studentService.GetById(id);
        IsNull(student);
        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Group,GroupId")] Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        try
        {
            _studentService.EditStudent(student);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var student = await _studentService.GetById(id);
        IsNull(student);
        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _studentService.GetById(id);
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
