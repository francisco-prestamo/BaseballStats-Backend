using FastEndpoints;
using BaseballStats.Application.DTOs;

namespace BaseballStats.Application.Features.DirectionStaff.GetAll;

public record GetAllDirectionStaffCommand : ICommand<List<DirectionStaffDto>>
{
    
}