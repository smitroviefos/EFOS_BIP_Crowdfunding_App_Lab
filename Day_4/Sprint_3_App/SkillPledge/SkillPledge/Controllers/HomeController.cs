using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillPledge.Data;
using SkillPledge.Models;
using SkillPledge.ViewModels;

namespace SkillPledge.Controllers;

public class HomeController(ApplicationDbContext db) : Controller
{
    // Sprint 3: the home page shows live numbers and the newest open projects
    public async Task<IActionResult> Index()
    {
        var today = DateTime.Today;

        var model = new HomeViewModel
        {
            ProjectCount = await db.Projects.CountAsync(),
            OpenProjectCount = await db.Projects.CountAsync(p => p.Status == ProjectStatus.Open && p.Deadline >= today),
            PledgeCount = await db.Pledges.CountAsync(),
            HoursPledged = await db.Pledges.SumAsync(p => p.Hours),
            LatestProjects = await db.Projects
                .AsNoTracking()
                .Include(p => p.SkillsNeeded)
                .Include(p => p.Pledges)
                .Where(p => p.Status == ProjectStatus.Open && p.Deadline >= today)
                .OrderByDescending(p => p.CreatedAt)
                .ThenByDescending(p => p.Id)
                .Take(3)
                .ToListAsync()
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Sprint 3: friendly pages for "404 Not Found" and other status codes (see Program.cs)
    [Route("Home/StatusCode/{code:int}")]
    public IActionResult StatusCodePage(int code)
    {
        Response.StatusCode = code;
        return View("StatusCode", code);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
