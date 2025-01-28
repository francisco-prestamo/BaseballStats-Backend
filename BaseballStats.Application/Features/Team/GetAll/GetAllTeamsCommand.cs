using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.GetAll;

public class GetAllTeamsCommand : ICommand<List<TeamDto>>
{
}