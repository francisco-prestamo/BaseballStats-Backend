using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.GetTeams;

public record GetTeamsCommand : ICommand<List<TeamWithDtIdDto>>;