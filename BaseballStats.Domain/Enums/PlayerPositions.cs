using System.ComponentModel.DataAnnotations;

namespace BaseballStats.Domain.Enums;

public enum PlayerPositions
{
    [Display(Name = "Lanzador")] Pitcher,
    [Display(Name = "Receptor")] Catcher,
    [Display(Name = "Primera Base")] FirstBase,
    [Display(Name = "Segunda Base")] SecondBase,
    [Display(Name = "Tercera Base")] ThirdBase,
    [Display(Name = "Campo Corto")] ShortStop,
    [Display(Name = "Jardín Izquierdo")] LeftField,
    [Display(Name = "Jardín Central")] CenterField,
    [Display(Name = "Jardín Derecho")] RightField,
    [Display(Name = "Bateador Designado")] DesignatedHitter
}