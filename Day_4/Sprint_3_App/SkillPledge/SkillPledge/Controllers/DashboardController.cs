using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillPledge.Data;
using SkillPledge.ViewModels;

namespace SkillPledge.Controllers;

/// <summary>
/// Sprint 3: "My SkillPledge" – my projects and the knowledge I offered, on one page.
/// </summary>
[Authorize]
public class DashboardController(ApplicationDbContext db, UserManager<IdentityUser> userManager) : Controller
{
    // GET: /Dashboard
    public async Task<IActionResult> Index()
    {
        var userId = userManager.GetUserId(User)!;

        var model = new DashboardViewModel
        {
            UserName = User.Identity?.Name ?? string.Empty,

            MyProjects = await db.Projects
                .AsNoTracking()
                .Include(p => p.SkillsNeeded)
                .Include(p => p.Pledges)
                .Where(p => p.OwnerId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(),

            MyPledges = await db.Pledges
                .AsNoTracking()
                .Include(p => p.Project)
                .Include(p => p.Skill)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync()
        };

        return View(model);
    }
}
