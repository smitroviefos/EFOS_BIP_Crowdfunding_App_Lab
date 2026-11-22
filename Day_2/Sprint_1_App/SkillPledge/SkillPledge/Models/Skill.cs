using System.ComponentModel.DataAnnotations;

namespace SkillPledge.Models;

/// <summary>
/// A skill that a project can need and a student can offer,
/// for example Marketing, UX design or Web development.
/// </summary>
public class Skill
{
    public int Id { get; set; }

    [Required]
    [StringLength(60)]
    public string Name { get; set; } = string.Empty;

    // Projects that need this skill (many-to-many, table ProjectSkills)
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
