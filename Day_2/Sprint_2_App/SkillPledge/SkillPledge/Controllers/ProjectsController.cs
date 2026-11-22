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
/// Sprint 2: full CRUD for projects.
/// Everyone can browse projects; only logged-in users can post one,
/// and only the owner can edit or delete it.
/// </summary>
public class ProjectsController(ApplicationDbContext db, UserManager<IdentityUser> userManager) : Controller
{
    // GET: /Projects?skillId=3&search=game
    public async Task<IActionResult> Index(int? skillId, string? search)
    {
        IQueryable<Project> query = db.Projects
            .AsNoTracking()
            .Include(p => p.Owner)
            .Include(p => p.SkillsNeeded)
            .Include(p => p.Pledges);

        if (skillId is not null)
        {
            query = query.Where(p => p.SkillsNeeded.Any(s => s.Id == skillId));
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(p => p.Title.Contains(term) || p.Description.Contains(term));
        }

        var projects = await query
            .OrderByDescending(p => p.CreatedAt)
            .ThenByDescending(p => p.Id)
            .ToListAsync();

        var skills = await db.Skills.AsNoTracking().OrderBy(s => s.Name).ToListAsync();
        ViewData["Skills"] = new SelectList(skills, nameof(Skill.Id), nameof(Skill.Name), skillId);
        ViewData["Search"] = search;

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

        ViewData["CurrentUserId"] = userManager.GetUserId(User);
        return View(project);
    }

    // GET: /Projects/Create
    [Authorize]
    public async Task<IActionResult> Create()
    {
        var form = new ProjectFormViewModel
        {
            Deadline = DateTime.Today.AddDays(30),
            AvailableSkills = await SkillOptionsAsync([])
        };
        return View(form);
    }

    // POST: /Projects/Create
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectFormViewModel form)
    {
        ValidateForm(form, isNew: true);
        if (!ModelState.IsValid)
        {
            form.AvailableSkills = await SkillOptionsAsync(form.SelectedSkillIds);
            return View(form);
        }

        var skills = await db.Skills.Where(s => form.SelectedSkillIds.Contains(s.Id)).ToListAsync();

        var project = new Project
        {
            Title = form.Title.Trim(),
            Description = form.Description.Trim(),
            Deadline = form.Deadline!.Value.Date,
            Status = ProjectStatus.Open,
            OwnerId = userManager.GetUserId(User)!,
            SkillsNeeded = skills
        };

        db.Projects.Add(project);
        await db.SaveChangesAsync();

        TempData["Message"] = "Your project was posted.";
        return RedirectToAction(nameof(Details), new { id = project.Id });
    }

    // GET: /Projects/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var project = await db.Projects
            .AsNoTracking()
            .Include(p => p.SkillsNeeded)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }

        if (!IsOwner(project))
        {
            return Forbid();
        }

        var selected = project.SkillsNeeded.Select(s => s.Id).ToList();
        var form = new ProjectFormViewModel
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Deadline = project.Deadline,
            Status = project.Status,
            SelectedSkillIds = selected,
            AvailableSkills = await SkillOptionsAsync(selected)
        };
        return View(form);
    }

    // POST: /Projects/Edit/5
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProjectFormViewModel form)
    {
        if (id != form.Id)
        {
            return NotFound();
        }

        var project = await db.Projects
            .Include(p => p.SkillsNeeded)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }

        if (!IsOwner(project))
        {
            return Forbid();
        }

        ValidateForm(form, isNew: false);
        if (!ModelState.IsValid)
        {
            form.AvailableSkills = await SkillOptionsAsync(form.SelectedSkillIds);
            return View(form);
        }

        project.Title = form.Title.Trim();
        project.Description = form.Description.Trim();
        project.Deadline = form.Deadline!.Value.Date;
        project.Status = form.Status;

        // Replace the list of needed skills with the new selection
        var skills = await db.Skills.Where(s => form.SelectedSkillIds.Contains(s.Id)).ToListAsync();
        project.SkillsNeeded.Clear();
        foreach (var skill in skills)
        {
            project.SkillsNeeded.Add(skill);
        }

        await db.SaveChangesAsync();

        TempData["Message"] = "Your changes were saved.";
        return RedirectToAction(nameof(Details), new { id = project.Id });
    }

    // GET: /Projects/Delete/5
    [Authorize]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var project = await db.Projects
            .AsNoTracking()
            .Include(p => p.Pledges)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }

        if (!IsOwner(project))
        {
            return Forbid();
        }

        return View(project);
    }

    // POST: /Projects/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var project = await db.Projects.FindAsync(id);
        if (project is null)
        {
            return NotFound();
        }

        if (!IsOwner(project))
        {
            return Forbid();
        }

        // Its pledges and skill links are deleted too (cascade delete)
        db.Projects.Remove(project);
        await db.SaveChangesAsync();

        TempData["Message"] = $"The project \"{project.Title}\" was deleted.";
        return RedirectToAction(nameof(Index));
    }

    private bool IsOwner(Project project) => project.OwnerId == userManager.GetUserId(User);

    // Rules that data annotations cannot express
    private void ValidateForm(ProjectFormViewModel form, bool isNew)
    {
        if (form.SelectedSkillIds.Count == 0)
        {
            ModelState.AddModelError(nameof(form.SelectedSkillIds), "Choose at least one skill your project needs.");
        }

        if (isNew && form.Deadline is not null && form.Deadline.Value.Date <= DateTime.Today)
        {
            ModelState.AddModelError(nameof(form.Deadline), "The deadline must be in the future.");
        }
    }

    private async Task<List<SelectListItem>> SkillOptionsAsync(IEnumerable<int> selectedIds)
    {
        var selected = selectedIds.ToHashSet();
        var skills = await db.Skills.AsNoTracking().OrderBy(s => s.Name).ToListAsync();
        return skills
            .Select(s => new SelectListItem(s.Name, s.Id.ToString(), selected.Contains(s.Id)))
            .ToList();
    }
}
