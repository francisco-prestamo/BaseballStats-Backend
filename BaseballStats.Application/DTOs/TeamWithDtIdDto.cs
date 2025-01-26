namespace BaseballStats.Application.DTOs;

public record TeamWithDtIdDto : TeamDto
{
    public long DtId { get; init; }
}