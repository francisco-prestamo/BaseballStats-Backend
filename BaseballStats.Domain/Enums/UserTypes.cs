using System.ComponentModel.DataAnnotations;

namespace BaseballStats.Domain.Enums;

public enum UserTypes
{
    [Display(Name = "Administrador")] Admin,
    [Display(Name = "Director Técnico")] TechnicalDirector,
    [Display(Name = "Periodista")] Journalist,
}