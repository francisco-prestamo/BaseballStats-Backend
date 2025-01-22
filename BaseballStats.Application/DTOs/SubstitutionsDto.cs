namespace BaseballStats.Application.DTOs;


public record GameSubstitutionsDto
{
    public List<SingleSubstitutionDto> Team1Substitutions { get; init; } = null!;
    public List<SingleSubstitutionDto> Team2Substitutions { get; init; } = null!;
}