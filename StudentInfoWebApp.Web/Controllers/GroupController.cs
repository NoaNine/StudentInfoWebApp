using Microsoft.AspNetCore.Mvc;
using StudentInfoWebApp.Core.Services.Interface;
using StudentInfoWebApp.DAL.Models;
using StudentInfoWebApp.Web.Models;
using System.Diagnostics;

namespace StudentInfoWebApp.Web.Controllers;

public class GroupController : Controller
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
    }

    public async Task<IActionResult> IndexAsync(int courseId)
    {
        var groups = await _groupService.GetAllGroupsAsync();
        if (courseId != 0)
        {
            groups = groups.Where(_ => _.CourseId == courseId);
        }
        return View(groups);
    }

    [HttpPost]
    public async Task<IActionResult> IndexAsync(string searchString)
    {
        var groups = await _groupService.GetAllGroupsAsync();
        if (!string.IsNullOrEmpty(searchString))
        {
            groups = groups.Where(_ => _.Name.Contains(searchString));
        }
        return View(groups);

    }

    public async Task<IActionResult> EditAsync(int id)
    {
        var group = await _groupService.GetByIdAsync(id);
        IsNull(group);
        return View(group);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(int id, [Bind("Id,Name,CourseId,Course,Students")] Group group)
    {
        if (id != group.Id)
        {
            return NotFound();
        }

        _groupService.EditGroup(group);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
        var group = await _groupService.GetByIdAsync(id);
        IsNull(group);
        return View(group);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAsync(int id)
    {
        var group = await _groupService.GetByIdAsync(id);
        IsNull(group);
        await _groupService.DeleteGroupAsync(group).ConfigureAwait(false);
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private IActionResult IsNull(Group group)
    {
        if (group == null)
        {
            return NotFound();
        }
        return Ok();
    }
}
