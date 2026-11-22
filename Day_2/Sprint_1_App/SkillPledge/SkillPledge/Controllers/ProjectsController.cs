using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillPledge.Data;

namespace SkillPledge.Controllers;

/// <summary>
/// Sprint 1: the "R" in CRUD – list and view projects.
/// Create, Edit and Delete follow in Sprint 2.
/// </summary>
public class ProjectsController(ApplicationDbContext db) : Controller
{
    // GET: /Projects
    public async Task<IActionResult> Index()
    {
        var projects = await db.Projects
            .AsNoTracking()
            .Include(p => p.Owner)
            .Include(p => p.SkillsNeeded)
            .Include(p => p.Pledges)
            .OrderByDescending(p => p.CreatedAt)
            .ThenByDescending(p => p.Id)
            .ToListAsync();

        return View(projects);
    }

    // GET: /Projects/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var project = await db.Projects
            .AsNoTracking()
            .Include(p => p.Owner)
            .Include(p => p.SkillsNeeded)
            .Include(p => p.Pledges).ThenInclude(pl => pl.User)
            .Include(p => p.Pledges).ThenInclude(pl => pl.Skill)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }

        return View(project);
    }
}
