using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SkillPledge.Models;

/// <summary>
/// A pledge is our "currency": a student offers one skill and a number of hours to a project.
/// </summary>
public class Pledge
{
    public int Id { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    // The student who offers help
    [Required]
    public string UserId { get; set; } = string.Empty;
    public IdentityUser? User { get; set; }

    public int SkillId { get; set; }
    public Skill? Skill { get; set; }

    [Range(1, 200)]
    public int Hours { get; set; }

    [StringLength(500)]
    public string? Message { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
