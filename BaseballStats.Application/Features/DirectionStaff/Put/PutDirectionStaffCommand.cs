using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.DirectionStaff.Put;

public record PutDirectionStaffCommand : ICommand<DirectionStaffDto>
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}