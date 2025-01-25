using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.DirectionStaff.Delete;

public record DeleteDirectionStaffCommand : ICommand<DirectionStaffDto>
{
    public long Id { get; set; }
}