using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillPledge.Data;
using SkillPledge.Models;
using SkillPledge.ViewModels;

namespace SkillPledge.Controllers;

/// <summary>
/// Sprint 2: students back a project with their knowledge.
/// A pledge = one skill + a number of hours, offered to one project.
/// </summary>
[Authorize]
public class PledgesController(ApplicationDbContext db, UserManager<IdentityUser> userManager) : Controller
{
    // GET: /Pledges/Create?projectId=5
    public async Task<IActionResult> Create(int projectId)
    {
        var project = await LoadProjectAsync(projectId);
        if (project is null)
        {
            return NotFound();
        }

        var problem = CheckCanPledge(project);
        if (problem is not null)
        {
            TempData["Error"] = problem;
            return RedirectToAction("Details", "Projects", new { id = projectId });
        }

        var form = new PledgeFormViewModel
        {
            ProjectId = project.Id,
            ProjectTitle = project.Title,
            SkillOptions = SkillOptions(project, null)
        };
        return View(form);
    }

    // POST: /Pledges/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PledgeFormViewModel form)
    {
        var project = await LoadProjectAsync(form.ProjectId);
        if (project is null)
        {
            return NotFound();
        }

        var problem = CheckCanPledge(project);
        if (problem is not null)
        {
            TempData["Error"] = problem;
            return RedirectToAction("Details", "Projects", new { id = project.Id });
        }

        if (form.SkillId is not null && project.SkillsNeeded.All(s => s.Id != form.SkillId))
        {
            ModelState.AddModelError(nameof(form.SkillId), "This project does not need that skill.");
        }

        if (!ModelState.IsValid)
        {
            form.ProjectTitle = project.Title;
            form.SkillOptions = SkillOptions(project, form.SkillId);
            return View(form);
        }

        db.Pledges.Add(new Pledge
        {
            ProjectId = project.Id,
            UserId = userManager.GetUserId(User)!,
            SkillId = form.SkillId!.Value,
            Hours = form.Hours,
            Message = string.IsNullOrWhiteSpace(form.Message) ? null : form.Message.Trim()
        });
        await db.SaveChangesAsync();

        TempData["Message"] = "Thank you! Your pledge was added.";
        return RedirectToAction("Details", "Projects", new { id = project.Id });
    }

    // POST: /Pledges/Withdraw/12 – a student removes their own pledge
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Withdraw(int id)
    {
        var pledge = await db.Pledges.FindAsync(id);
        if (pledge is null)
        {
            return NotFound();
        }

        if (pledge.UserId != userManager.GetUserId(User))
        {
            return Forbid();
        }

        db.Pledges.Remove(pledge);
        await db.SaveChangesAsync();

        TempData["Message"] = "Your pledge was withdrawn.";
        return RedirectToAction("Details", "Projects", new { id = pledge.ProjectId });
    }

    private Task<Project?> LoadProjectAsync(int id) =>
        db.Projects
            .Include(p => p.SkillsNeeded)
            .FirstOrDefaultAsync(p => p.Id == id);

    // Business rules: no pledges to your own project, and only to open projects
    private string? CheckCanPledge(Project project)
    {
        if (project.OwnerId == userManager.GetUserId(User))
        {
            return "You cannot pledge to your own project.";
        }

        if (project.Status != ProjectStatus.Open)
        {
            return "This project is not open for pledges.";
        }

        return null;
    }

    private static List<SelectListItem> SkillOptions(Project project, int? selectedId) =>
        project.SkillsNeeded
            .OrderBy(s => s.Name)
            .Select(s => new SelectListItem(s.Name, s.Id.ToString(), s.Id == selectedId))
            .ToList();
}
