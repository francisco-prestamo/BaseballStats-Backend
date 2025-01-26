using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.Post;

public record PostTeamCommand : TeamAdminDto, ICommand<TeamAdminDto>;