using System.ComponentModel.DataAnnotations;

namespace BaseballStats.Domain.Enums;

public enum UserTypes
{
    [Display(Name = "Admin")] Admin,
    [Display(Name = "Technical-Director")] TechnicalDirector,
    [Display(Name = "Journalist")] Journalist,
}