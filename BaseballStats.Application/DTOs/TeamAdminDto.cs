namespace BaseballStats.Application.DTOs;

public record TeamAdminDto : TeamDto
{
    public long DtId { get; init; }
}