namespace SkillPledge.Models;

/// <summary>
/// Sprint 3: the business rules of a project in one place.
/// Before, the same rules were repeated in controllers and views – now everyone uses these methods.
/// These are plain C# methods, so they are not stored in the database (no migration needed).
/// </summary>
public static class ProjectRules
{
    /// <summary>The deadline day itself still counts – the project closes the day after.</summary>
    public static bool IsDeadlinePassed(this Project project) =>
        project.Deadline.Date < DateTime.Today;

    public static int DaysLeft(this Project project) =>
        Math.Max(0, (project.Deadline.Date - DateTime.Today).Days);

    /// <summary>Only open projects whose deadline has not passed accept new pledges.</summary>
    public static bool AcceptsPledges(this Project project) =>
        project.Status == ProjectStatus.Open && !project.IsDeadlinePassed();

    /// <summary>A skill is covered when at least one pledge offers it.</summary>
    public static bool IsSkillCovered(this Project project, int skillId) =>
        project.Pledges.Any(p => p.SkillId == skillId);

    public static int SkillsCoveredCount(this Project project) =>
        project.SkillsNeeded.Count(s => project.IsSkillCovered(s.Id));

    public static int SkillsCoveredPercent(this Project project) =>
        project.SkillsNeeded.Count == 0
            ? 0
            : (int)Math.Round(100.0 * project.SkillsCoveredCount() / project.SkillsNeeded.Count);

    public static int TotalHours(this Project project) =>
        project.Pledges.Sum(p => p.Hours);
}
