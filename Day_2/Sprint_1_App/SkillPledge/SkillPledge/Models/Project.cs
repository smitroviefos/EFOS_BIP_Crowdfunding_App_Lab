using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SkillPledge.Models;

/// <summary>
/// A student project that asks the crowd for knowledge instead of money.
/// </summary>
public class Project
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime Deadline { get; set; }

    public ProjectStatus Status { get; set; } = ProjectStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // The student who posted the project
    [Required]
    public string OwnerId { get; set; } = string.Empty;
    public IdentityUser? Owner { get; set; }

    // Skills the project is looking for (many-to-many, table ProjectSkills)
    public ICollection<Skill> SkillsNeeded { get; set; } = new List<Skill>();

    // Offers of knowledge from other students
    public ICollection<Pledge> Pledges { get; set; } = new List<Pledge>();
}

public enum ProjectStatus
{
    Open,
    InProgress,
    Completed
}
