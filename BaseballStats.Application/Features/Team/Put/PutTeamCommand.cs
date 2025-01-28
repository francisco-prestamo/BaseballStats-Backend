using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.Put;

public record PutTeamCommand : TeamAdminDto, ICommand<TeamAdminDto>;