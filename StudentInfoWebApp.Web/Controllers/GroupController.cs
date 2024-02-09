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

    public async Task<IActionResult> Index(int courseId)
    {
        try
        {
            var groups = await _groupService.GetAllGroups();
            if (courseId != 0)
            {
                groups = groups.Where(_ => _.CourseId == courseId);
            }
            return View(groups);
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
            var groups = await _groupService.GetAllGroups();
            if (!string.IsNullOrEmpty(searchString))
            {
                groups = groups.Where(_ => _.Name.Contains(searchString));
            }
            return View(groups);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var group = await _groupService.GetById(id);
        IsNull(group);
        return View(group);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CourseId,Course,Students")] Group group)
    {
        if (id != group.Id)
        {
            return NotFound();
        }

        try
        {
            _groupService.EditGroup(group);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var group = await _groupService.GetById(id);
        IsNull(group);
        return View(group);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var group = await _groupService.GetById(id);
        IsNull(group);
        try
        {
            _groupService.DeleteGroup(group);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
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
