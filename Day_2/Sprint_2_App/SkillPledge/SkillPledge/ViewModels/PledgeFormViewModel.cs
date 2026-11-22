using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SkillPledge.ViewModels;

/// <summary>
/// The data of the "Offer your knowledge" form.
/// </summary>
public class PledgeFormViewModel
{
    public int ProjectId { get; set; }

    [ValidateNever]
    public string ProjectTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Choose the skill you want to offer.")]
    [Display(Name = "Skill you offer")]
    public int? SkillId { get; set; }

    [Range(1, 200, ErrorMessage = "Offer between 1 and 200 hours.")]
    [Display(Name = "Hours")]
    public int Hours { get; set; } = 5;

    [StringLength(500)]
    [Display(Name = "Message to the project owner")]
    public string? Message { get; set; }

    // Only the skills this project needs
    [ValidateNever]
    public List<SelectListItem> SkillOptions { get; set; } = new();
}
