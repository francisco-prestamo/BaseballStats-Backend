using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Pitcher.GetAll
{
    public record GetAllPitchersCommand : ICommand<List<PitcherDto>>
    {}
}