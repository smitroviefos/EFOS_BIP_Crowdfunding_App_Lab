using SkillPledge.Models;

namespace SkillPledge.ViewModels;

/// <summary>
/// Sprint 3: everything the "My SkillPledge" page shows for the logged-in student.
/// </summary>
public class DashboardViewModel
{
    public string UserName { get; set; } = string.Empty;

    // Projects I posted
    public List<Project> MyProjects { get; set; } = [];

    // Knowledge I offered to other projects
    public List<Pledge> MyPledges { get; set; } = [];

    public int HoursReceived => MyProjects.Sum(p => p.TotalHours());
    public int HoursPledged => MyPledges.Sum(p => p.Hours);
}
