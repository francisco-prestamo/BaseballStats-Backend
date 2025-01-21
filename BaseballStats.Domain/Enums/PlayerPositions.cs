using System.ComponentModel.DataAnnotations;

namespace BaseballStats.Domain.Enums;

public enum PlayerPositions
{
    [Display(Name = "Pitcher")] Pitcher,
    [Display(Name = "Catcher")] Catcher,
    [Display(Name = "First-Base")] FirstBase,
    [Display(Name = "Second-Base")] SecondBase,
    [Display(Name = "Third-Base")] ThirdBase,
    [Display(Name = "Shortstop")] ShortStop,
    [Display(Name = "Left-Field")] LeftField,
    [Display(Name = "Center-Field")] CenterField,
    [Display(Name = "Right-Field")] RightField,
    [Display(Name = "Designated-Hitter")] DesignatedHitter
}