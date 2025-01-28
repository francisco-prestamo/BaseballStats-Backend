using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.TechnicalDirector.GetAll
{
    public record GetAllTechnicalDirectorsCommand : ICommand<List<RegisteredUserDto>>
    {
    }
}