using Microsoft.AspNetCore.Mvc;
using StudentInfoWebApp.Core.Services;
using StudentInfoWebApp.Core.Services.Interface;
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

    public async Task<IActionResult> Index()
    {
        try
        {
            var groups = await _groupService.GetAllGroups();
            return View(groups);
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
