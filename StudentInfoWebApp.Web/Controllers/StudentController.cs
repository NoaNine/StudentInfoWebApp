using Microsoft.AspNetCore.Mvc;
using StudentInfoWebApp.Core.Services;
using StudentInfoWebApp.Core.Services.Interface;
using StudentInfoWebApp.Web.Models;
using System.Diagnostics;

namespace StudentInfoWebApp.Web.Controllers
{
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
