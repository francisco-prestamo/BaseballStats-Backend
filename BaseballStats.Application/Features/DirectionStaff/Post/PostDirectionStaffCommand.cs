using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.DirectionStaff.Post;

public record PostDirectionStaffCommand : ICommand<DirectionStaffDto>
{
    public string Name { get; set; } = null!;
}