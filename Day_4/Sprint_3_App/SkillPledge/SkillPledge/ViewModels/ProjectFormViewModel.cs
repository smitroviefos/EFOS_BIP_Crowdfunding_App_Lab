using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillPledge.Models;

namespace SkillPledge.ViewModels;

/// <summary>
/// The data of the Create and Edit project forms.
/// A view model only contains the fields the form may change – this protects
/// fields such as OwnerId or CreatedAt from being changed by a user (overposting).
/// </summary>
public class ProjectFormViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    [Display(Name = "Project title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Deadline")]
    public DateTime? Deadline { get; set; }

    [Display(Name = "Status")]
    public ProjectStatus Status { get; set; } = ProjectStatus.Open;

    [Display(Name = "Skills needed")]
    public List<int> SelectedSkillIds { get; set; } = new();

    // Filled by the controller to show the checkboxes – not sent by the form
    [ValidateNever]
    public List<SelectListItem> AvailableSkills { get; set; } = new();
}
