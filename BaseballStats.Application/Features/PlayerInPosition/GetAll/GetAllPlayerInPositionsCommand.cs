using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInPosition.GetAll;

public record GetAllPlayerInPositionsCommand : ICommand<List<PlayerInPositionCRUDDto>> {}
