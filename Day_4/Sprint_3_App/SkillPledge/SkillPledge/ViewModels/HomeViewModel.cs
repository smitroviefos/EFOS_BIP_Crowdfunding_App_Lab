using SkillPledge.Models;

namespace SkillPledge.ViewModels;

/// <summary>
/// Sprint 3: live numbers and the newest open projects for the home page.
/// Nice for the pitch – the home page shows that the platform is alive.
/// </summary>
public class HomeViewModel
{
    public int ProjectCount { get; set; }
    public int OpenProjectCount { get; set; }
    public int PledgeCount { get; set; }
    public int HoursPledged { get; set; }
    public List<Project> LatestProjects { get; set; } = [];
}
