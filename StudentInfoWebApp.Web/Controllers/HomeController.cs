using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using StudentInfoWebApp.Core.Services.Interface;
using StudentInfoWebApp.DAL.Models;
using StudentInfoWebApp.Web.Models;
using System.Diagnostics;

namespace StudentInfoWebApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly ICourseService _courseService;

    public HomeController(ICourseService courseService)
    {
        _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
    }

    public async Task<IActionResult> IndexAsync()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return View(courses);
    }

    public async Task<IActionResult> EditAsync(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        IsNull(course);
        return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(int id, [Bind("Id,Name,Description,Groups")] Course course)
    {
        if (id != course.Id)
        {
            return NotFound();
        }

        _courseService.EditCourse(course);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        IsNull(course);

        return View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAsync(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        IsNull(course);
        _courseService.DeleteCourse(course);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private IActionResult IsNull(Course course)
    {
        if (course == null)
        {
            return NotFound();
        }
        return Ok();
    }
}
